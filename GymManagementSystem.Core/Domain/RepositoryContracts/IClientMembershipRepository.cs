using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.Dashboard;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IClientMembershipRepository : IRepository<ClientMembershipResponse, ClientMembership>
{
    Task<IEnumerable<ClientMembershipResponse>> GetAllClientMemberships(Guid id);
    Task<ClientMembership?> GetActiveClientMembershipByClientId(Guid clientId);
    Task<ClientMembership?> GetActiveClientMembershipById(Guid clientMembershipId);
    Task<int> GetActiveClientMembershipsCountAsync(DateTime? from);
    Task<List<PointResponse>> GetAllClientMembershipsOverTime();
}
