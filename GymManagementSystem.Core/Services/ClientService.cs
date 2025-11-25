using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ClientService<Entity> : IClientService
{
    private readonly IClientRepository _repository;
    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<ClientResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Client> clients = await _repository.GetAllAsync(cancellationToken);
        IEnumerable<ClientResponse> clientResponseList = clients.Select(client => client.ToClientResponse());
        return Result<IEnumerable<ClientResponse>>.Success(clientResponseList, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientInfoResponse>> UpdateAsync(Guid id, ClientUpdateRequest request, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return Result<ClientInfoResponse>.Failure("Invalid ID", StatusCodeEnum.BadRequest);
        }

        Client client = request.ToClient();
        Client? updatedClient = await _repository.UpdateAsync(id, client, cancellationToken);

        if(updatedClient == null)
        {
            return Result<ClientInfoResponse>.Failure("Client not found, updation failed", StatusCodeEnum.NotFound);
        }

        ClientInfoResponse clientResponse = updatedClient.ToClientInfoResponse();

        return Result<ClientInfoResponse>.Success(clientResponse,StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientInfoResponse>> CreateAsync(ClientAddRequest request, CancellationToken cancellationToken)
    {
       Client client = request.ToClient();
       Client createdClient = await _repository.CreateAsync(client, cancellationToken);

       ClientInfoResponse clientResponse = createdClient.ToClientInfoResponse();
       return Result<ClientInfoResponse>.Success(clientResponse, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientDetailsResponse>> GetByIdAsync(Guid id, bool isActiveOnly,CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return Result<ClientDetailsResponse>.Failure("Invalid id", StatusCodeEnum.BadRequest);
        }

        Client? client = await _repository.GetByIdAsync(id, cancellationToken);

        if (client == null)
        {
            return Result<ClientDetailsResponse>.Failure("Client not found", StatusCodeEnum.NotFound);
        }
       
        ClientDetailsResponse clientResponse = client.ToClientDetailsResponse();

        return Result<ClientDetailsResponse>.Success(clientResponse, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<ClientInfoResponse>>> LookUpClientsAsync(string query, Guid scheduledClassId)
    {
      IEnumerable<Client> searchedClients = await _repository.LookUpClientsAsync(query, scheduledClassId);
      return Result<IEnumerable<ClientInfoResponse>>.Success(searchedClients.Select(item => item.ToClientInfoResponse()));
    }
}
