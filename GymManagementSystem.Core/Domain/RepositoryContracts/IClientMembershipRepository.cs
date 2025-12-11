using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClientMembership;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IClientMembershipRepository : IRepository<ClientMembershipResponse, ClientMembership>
{
    Task<IEnumerable<ClientMembershipResponse>> GetAllClientMemberships(Guid id);
}
