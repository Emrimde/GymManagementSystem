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
    private readonly IRepository<Client> _repository;
    // private readonly IContractRepository _contractRepository;
    public ClientService(IRepository<Client> repository)
    {
        _repository = repository;
        //_contractRepository = contractRepository;
    }

    public async Task<Result<IEnumerable<ClientResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Client> clients = await _repository.GetAllAsync(cancellationToken);
        IEnumerable<ClientResponse> clientResponseList = clients.Select(client => client.ToClientResponse());
        return Result<IEnumerable<ClientResponse>>.Success(clientResponseList, StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientResponse>> UpdateAsync(Guid id, ClientUpdateRequest request, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            return Result<ClientResponse>.Failure("Invalid ID", StatusCodeEnum.BadRequest);
        }

        Client client = request.ToClient();
        Client? updatedClient = await _repository.UpdateAsync(id, client, cancellationToken);

        if(updatedClient == null)
        {
            return Result<ClientResponse>.Failure("Client not found, updation failed", StatusCodeEnum.NotFound);
        }

        ClientResponse clientResponse = updatedClient.ToClientResponse();

        return Result<ClientResponse>.Success(clientResponse,StatusCodeEnum.Ok);
    }

    public async Task<Result<ClientResponse>> CreateAsync(ClientAddRequest request, CancellationToken cancellationToken)
    {
       Client client = request.ToClient();
       Client createdClient = await _repository.CreateAsync(client, cancellationToken);

        ClientResponse clientResponse = createdClient.ToClientResponse();
       return Result<ClientResponse>.Success(clientResponse, StatusCodeEnum.Ok);
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
       
        //client.ClientMemberships =  client.ClientMemberships
        //         .Where(item => item.IsActive)
        //         .ToList();

        ClientDetailsResponse clientResponse = client.ToClientDetailsResponse();

        return Result<ClientDetailsResponse>.Success(clientResponse, StatusCodeEnum.Ok);
    }
}
