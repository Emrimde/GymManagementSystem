using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IClientService
{
    Task<Result<ClientResponse>> CreateAsync(ClientAddRequest entity, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ClientResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<ClientDetailsResponse>> GetByIdAsync(Guid id, bool isActiveOnly, CancellationToken cancellationToken);
    Task<Result<ClientResponse>> UpdateAsync(Guid id, ClientUpdateRequest entity, CancellationToken cancellationToken);
}
