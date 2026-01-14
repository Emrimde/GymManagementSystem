using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.ClientMembership;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IClientMembershipRepository : IRepository<ClientMembershipResponse, ClientMembership>
{
    Task<IEnumerable<ClientMembershipResponse>> GetAllClientMemberships(Guid id);
    Task<ClientMembership?> GetActiveClientMembershipByClientId(Guid clientId);
    Task<ClientMembership?> GetActiveClientMembershipById(Guid clientMembershipId);
    Task<int> GetActiveClientMembershipsCountAsync(DateTime? from);
    Task<List<PointResponse>> GetAllClientMembershipsOverTime();
    Task<ClientMembershipWebResponse?> GetClientMembershipByClientIdAsync(Guid clientId);
    Task<PageResult<ClientMembershipResponse>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null);
}
