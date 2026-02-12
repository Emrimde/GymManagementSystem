using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Client.QueryDto;
using GymManagementSystem.Core.DTO.Email;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO;
using GymManagementSystem.Core.WebDTO.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace GymManagementSystem.Core.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;
    private readonly IVisitRepository _visitRepo;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _http;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientMembershipRepository _clientMembershipRepository;
    private readonly IPersonRepository _personRepo;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    public ClientService(IClientRepository repository, IVisitRepository visitRepository, UserManager<User> userManager, IHttpContextAccessor http, IUnitOfWork unitOfWork, IClientMembershipRepository clientMembershipRepository, IConfiguration configuration, IEmailService emailService, IPersonRepository personRepo)
    {
        _repository = repository;
        _visitRepo = visitRepository;
        _userManager = userManager;
        _http = http;
        _unitOfWork = unitOfWork;
        _clientMembershipRepository = clientMembershipRepository;
        _configuration = configuration;
        _emailService = emailService;
        _personRepo = personRepo;
    }

    public async Task<PageResult<ClientResponse>> GetAllAsync(GetClientQueryDto query)
    {
        PageResult<ClientResponse> clients = await _repository.GetAllAsync(isActive: query.IsActive, searchText: query.SearchText, page: query.Page);
        return clients;
    }

    public async Task<Result<ClientInfoResponse>> UpdateAsync(Guid id, ClientUpdateRequest request)
    {
        if (id == Guid.Empty)
        {
            return Result<ClientInfoResponse>.Failure("Invalid ID", StatusCodeEnum.BadRequest);
        }
        Client? clientt = await _repository.GetByIdAsync(id);
        if (clientt == null)
        {
            return Result<ClientInfoResponse>.Failure("Client not found", StatusCodeEnum.NotFound);
        }

        bool exist = await _repository.ExistsByPhoneAsync(request.PhoneNumber, id);

        if (exist)
        {
            return Result<ClientInfoResponse>.Failure("Client with the same phone number already exists", StatusCodeEnum.BadRequest);
        }

        bool personExist = await _personRepo.ExistsByPhoneAsync(request.PhoneNumber, null);

        if (personExist)
        {
            return Result<ClientInfoResponse>.Failure("Person from staff with the same phone number already exists", StatusCodeEnum.BadRequest);
        }

        clientt.ModifyClient(request);

        await _unitOfWork.SaveChangesAsync();

        ClientInfoResponse clientResponse = clientt.ToClientInfoResponse();

        return Result<ClientInfoResponse>.Success(clientResponse, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientInfoResponse>> CreateAsync(ClientAddRequest request)
    {
        Client client = request.ToClient();
        DateTime today = DateTime.UtcNow;
        int age = today.Year - client.DateOfBirth.Year;
        if (client.DateOfBirth > today.AddYears(-age))
        {
            age--;
        }

        bool exists = await _repository.ExistsByEmailOrPhoneAsync(request.Email, request.PhoneNumber);
        if (exists)
        {
            Result<ClientInfoResponse>.Failure("Client with the same email or phone number already exists", StatusCodeEnum.BadRequest);
        }
        bool existsInEmployee = await _personRepo.ExistsByEmailOrPhoneAsync(request.Email, request.PhoneNumber);
        if (existsInEmployee)
        {
            Result<ClientInfoResponse>.Failure("Person with the same email or phone number already exists in employees", StatusCodeEnum.BadRequest);
        }


        client.HasParentalConsent = age < 18 ? true : null;
        _repository.CreateAsync(client);
        await _unitOfWork.SaveChangesAsync();
        User user = new User()
        {
            UserName = client.Email,
            ClientId = client.Id,
            Email = client.Email,
            EmailConfirmed = false
        };


        IdentityResult createResult = await _userManager.CreateAsync(user);
        if (!createResult.Succeeded)
        {
            string error = string.Join('\n', createResult.Errors.Select(item => item.Description));
            return Result<ClientInfoResponse>.Failure($"{error}", StatusCodeEnum.InternalServerError);
        }

        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        string encodedToken = Uri.EscapeDataString(token);

        string link =
            $"{_configuration["App:WebUrl"]}/activate-account" +
            $"?userId={user.Id}&token={encodedToken}";

        EmailRequest emailRequest = new EmailRequest
        {
            To = user.Email,
            Subject = "Activate your account",
            Body = $@"
        <p>Click the link below to activate your account.</p>
        <p>
            <a href='{link}'>Activate account</a>
        </p>
        <p>If this wasn't you, ignore this message.</p>"
        };

        await _emailService.SendLink(emailRequest);

        await _userManager.AddToRoleAsync(user, "Client");
        ClientInfoResponse clientResponse = client.ToClientInfoResponse();
        await _unitOfWork.SaveChangesAsync();
        return Result<ClientInfoResponse>.Success(clientResponse, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientDetailsResponse>> GetByIdAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            return Result<ClientDetailsResponse>.Failure("Invalid id", StatusCodeEnum.BadRequest);
        }

        ClientDetailsResponse? client = await _repository.GetClientDetailsAsync(id);

        if (client == null)
        {
            return Result<ClientDetailsResponse>.Failure("Client not found", StatusCodeEnum.NotFound);
        }

        int visitsCount = await _visitRepo.GetTotalVisitsByClientId(id);
        DateTime lastVisit = await _visitRepo.GetLastVisitDateByClientId(id);

        return Result<ClientDetailsResponse>.Success(client, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<ClientInfoResponse>>> LookUpClientsAsync(string query, Guid? scheduledClassId = null)
    {
        IEnumerable<Client> searchedClients = await _repository.LookUpClientsAsync(query, scheduledClassId);
        return Result<IEnumerable<ClientInfoResponse>>.Success(searchedClients.Select(item => item.ToClientInfoResponse()));
    }

    public Result<ClientAgeValidationResponse> ValidateClientAgeAsync(ClientAgeValidationRequest entity)
    {
        DateTime today = DateTime.UtcNow;
        int age = today.Year - entity.DateOfBirth.Year;
        if (entity.DateOfBirth > today.AddYears(age)) age--;
        ClientAgeValidationResponse response = new ClientAgeValidationResponse()
        {
            Age = age
        };
        return Result<ClientAgeValidationResponse>.Success(response, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientInfoResponse>> GetClientFullNameByIdAsync(Guid id)
    {
        ClientInfoResponse? dto = await _repository.GetClientFullNameByIdAsync(id);
        if (dto == null)
        {
            return Result<ClientInfoResponse>.Failure("Error during getting client data", StatusCodeEnum.NotFound);
        }
        return Result<ClientInfoResponse>.Success(dto, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientDetailsWebResponse>> GetClientProfileInfoAsync()
    {
        string? claim = _http.HttpContext?.User.FindFirst("client_id")?.Value;
        if (!Guid.TryParse(claim, out var clientId))
        {
            return Result<ClientDetailsWebResponse>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
        }

        ClientDetailsWebResponse? dto = await _repository.GetClientProfileInfoAsync(clientId);
        if (dto == null)
        {
            return Result<ClientDetailsWebResponse>.Failure("Error during loading client", StatusCodeEnum.BadRequest);
        }
        return Result<ClientDetailsWebResponse>.Success(dto, StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> CreateAccountAsync(ClientWebAddRequest entity)
    {
        Client client = entity.ToClient();
        _repository.CreateAsync(client);
        User user = new User()
        {
            UserName = client.FirstName + client.LastName,
            ClientId = client.Id,
            Email = client.Email,
        };
        var createResult = await _userManager.CreateAsync(user, entity.Password);
        if (!createResult.Succeeded)
        {
            return Result<Unit>.Failure($"{createResult.Errors}", StatusCodeEnum.InternalServerError);
        }

        await _userManager.AddToRoleAsync(user, "Member");
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> UpdateWebClientInfoAsync(ClientWebUpdateRequest updateRequest)
    {
        string? claim = _http.HttpContext?.User.FindFirst("client_id")?.Value;
        if (!Guid.TryParse(claim, out var clientId))
        {
            return Result<Unit>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
        }

        Client client = updateRequest.ToClient();
        client.Id = clientId;
        client.DateOfBirth = DateTime.SpecifyKind(updateRequest.DateOfBirth, DateTimeKind.Utc);
        await _repository.UpdateClientAsync(client);
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.NoContent);
    }

    public async Task<Result<ClientMembershipInformationResponse>> GetClientContextAsync()
    {
        string? claim = _http.HttpContext?.User.FindFirst("client_id")?.Value;
        if (!Guid.TryParse(claim, out var clientId))
        {
            return Result<ClientMembershipInformationResponse>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
        }
        ClientMembership? clientMembership = await _clientMembershipRepository.GetActiveClientMembershipByClientId(clientId);
        ClientMembershipInformationResponse response = new ClientMembershipInformationResponse();
        if (clientMembership == null)
        {
            response.HasActiveMembership = false;
        }
        else
        {
            response.HasActiveMembership = true;
            response.EndDate = clientMembership.EndDate.HasValue ? clientMembership.EndDate?.ToString("dd.MM.yyyy") : "indefinite time";
            response.StartDate = clientMembership.StartDate.ToString("dd.MM.yyyy");
        }
        return Result<ClientMembershipInformationResponse>.Success(response, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientEditResponse>> GetByIdForEditAsync(Guid id)
    {
        Client? client = await _repository.GetByIdAsync(id);
        if (client == null)
        {
            return Result<ClientEditResponse>.Failure("Client not found", StatusCodeEnum.NotFound);
        }

        ClientEditResponse clientEditResponse = new ClientEditResponse()
        {
            City = client.City,
            Email = client.Email,
            LastName = client.LastName,
            PhoneNumber = client.PhoneNumber,
            Street = client.StreetAddress
        };

        return Result<ClientEditResponse>.Success(clientEditResponse, StatusCodeEnum.Ok);
    }
}
