using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts
{
    public interface IClientMembershipService
    {
        Task<Result<ClientMembershipInfoResponse>> CreateAsync(ClientMembershipAddRequest entity, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ClientMembershipResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<ClientMembershipResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<ClientMembershipResponse>> UpdateAsync(Guid id, ClientMembershipUpdateRequest entity, CancellationToken cancellationToken);
    }
}