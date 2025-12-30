using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace GymManagementSystem.Core.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;
    private readonly IVisitRepository _visitRepo;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _http;
    public ClientService(IClientRepository repository,IVisitRepository visitRepository, UserManager<User> userManager, IHttpContextAccessor http)
    {
        _repository = repository;
        _visitRepo = visitRepository;
        _userManager = userManager;
        _http = http;
    }

    public async Task<PageResult<ClientResponse>> GetAllAsync(string? searchText, int page)
    {
        PageResult<ClientResponse> clients = await _repository.GetAllAsync(searchText:searchText, page: page);
        return clients;
    }

    public async Task<Result<ClientInfoResponse>> UpdateAsync(Guid id, ClientUpdateRequest request)
    {
        if (id == Guid.Empty)
        {
            return Result<ClientInfoResponse>.Failure("Invalid ID", StatusCodeEnum.BadRequest);
        }

        Client client = request.ToClient();
        Client? updatedClient = await _repository.UpdateAsync(id, client);

        if (updatedClient == null)
        {
            return Result<ClientInfoResponse>.Failure("Client not found, updation failed", StatusCodeEnum.NotFound);
        }

        ClientInfoResponse clientResponse = updatedClient.ToClientInfoResponse();

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
        Client createdClient = await _repository.CreateAsync(client);
        User user = new User()
        {
            UserName = createdClient.FirstName + createdClient.LastName,
            ClientId = createdClient.Id,
            Email = createdClient.Email,
        };
        var createResult = await _userManager.CreateAsync(user, "example");
        if (!createResult.Succeeded)
        {
            return Result<ClientInfoResponse>.Failure($"{createResult.Errors}", StatusCodeEnum.InternalServerError);
        }
        
        await _userManager.AddToRoleAsync(user, "Member");
        ClientInfoResponse clientResponse = createdClient.ToClientInfoResponse();
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

    public async Task<Result<ClientDetailsWebResponse>> GetClientDetailsByUserIdAsync()
    {
        var sub = _http.HttpContext?.User.FindFirst("sub")?.Value;
        if (!Guid.TryParse(sub, out var userId))
            return Result<ClientDetailsWebResponse>.Failure("Error, token not found dsfsd", StatusCodeEnum.Unauthorized);

        ClientDetailsWebResponse? dto = await _repository.GetClientByUserIdAsync(userId);
        if(dto == null)
        {
            return Result<ClientDetailsWebResponse>.Failure("Error during loading client", StatusCodeEnum.BadRequest);
        }
        return Result<ClientDetailsWebResponse>.Success(dto, StatusCodeEnum.Ok);
    }
}
