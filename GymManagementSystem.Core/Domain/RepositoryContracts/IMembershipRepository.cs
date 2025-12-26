using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Membership;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IMembershipRepository : IRepository<MembershipResponse, Membership>
{
    public Task<IEnumerable<MembershipResponse>> GetAllMemberships();
    Task<MembershipInfoResponse?> GetMembershipNameAsync(Guid membershipId);
}
