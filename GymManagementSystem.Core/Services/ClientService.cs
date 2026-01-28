using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Client.QueryDto;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO;
using GymManagementSystem.Core.WebDTO.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Core.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;
    private readonly IVisitRepository _visitRepo;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _http;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClientMembershipRepository _clientMembershipRepository;
    public ClientService(IClientRepository repository,IVisitRepository visitRepository, UserManager<User> userManager, IHttpContextAccessor http, IUnitOfWork unitOfWork, IClientMembershipRepository clientMembershipRepository)
    {
        _repository = repository;
        _visitRepo = visitRepository;
        _userManager = userManager;
        _http = http;
        _unitOfWork = unitOfWork;
        _clientMembershipRepository = clientMembershipRepository;
    }

    public async Task<PageResult<ClientResponse>> GetAllAsync(GetClientQueryDto query)
    {
        PageResult<ClientResponse> clients = await _repository.GetAllAsync(isActive:query.IsActive, searchText:query.SearchText, page: query.Page);
        return clients;
    }

    public async Task<Result<ClientInfoResponse>> UpdateAsync(Guid id, ClientUpdateRequest request)
    {
        if (id == Guid.Empty)
        {
            return Result<ClientInfoResponse>.Failure("Invalid ID", StatusCodeEnum.BadRequest);
        }
        Client? clientt = await _repository.GetByIdAsync(id);
        if(clientt == null)
        {
            return Result<ClientInfoResponse>.Failure("Client not found", StatusCodeEnum.NotFound);
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

        client.HasParentalConsent = age < 18 ? true : null;
        _repository.CreateAsync(client);
        User user = new User()
        {
            UserName = client.FirstName + client.LastName,
            ClientId = client.Id,
            Email = client.Email,
        };
        var createResult = await _userManager.CreateAsync(user, "example");
        if (!createResult.Succeeded)
        {
            string error = string.Join('\n', createResult.Errors.Select(item => item.Description));
            return Result<ClientInfoResponse>.Failure($"{error}", StatusCodeEnum.InternalServerError);
        }
        
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

        Client? client = await _repository.GetByIdAsync(id);

        if (client == null)
        {
            return Result<ClientDetailsResponse>.Failure("Client not found", StatusCodeEnum.NotFound);
        }

        int visitsCount = await _visitRepo.GetTotalVisitsByClientId(id);
        DateTime lastVisit = await _visitRepo.GetLastVisitDateByClientId(id);

        ClientDetailsResponse clientResponse = client.ToClientDetailsResponse();
        clientResponse.LastVisitDate = lastVisit == DateTime.MinValue ? "0 visits" : lastVisit.ToString("dd.MM.yyyy - HH:mm");
        clientResponse.TotalVisits = visitsCount;


        return Result<ClientDetailsResponse>.Success(clientResponse, StatusCodeEnum.Ok);
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
        if(dto == null)
        {
            return Result<ClientInfoResponse>.Failure("Error during getting client data");
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
        if(dto == null)
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
        var createResult = await _userManager.CreateAsync(user,entity.Password);
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
        ClientMembership? clientMembership =  await _clientMembershipRepository.GetActiveClientMembershipByClientId(clientId);
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
