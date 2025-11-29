using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IClientService
{
    Task<Result<ClientInfoResponse>> CreateAsync(ClientAddRequest entity);
    Task<Result<IEnumerable<ClientResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<ClientDetailsResponse>> GetByIdAsync(Guid id, bool isActiveOnly, CancellationToken cancellationToken);
    Task<Result<IEnumerable<ClientInfoResponse>>> LookUpClientsAsync(string query, Guid? scheduledClassId = null);
    Task<Result<ClientInfoResponse>> UpdateAsync(Guid id, ClientUpdateRequest entity);
}
