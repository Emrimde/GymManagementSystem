using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.WebDTO.Membership;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IMembershipRepository : IRepository<MembershipResponse, Membership>
{
    public Task<IEnumerable<MembershipResponse>> GetAllMemberships();
    Task<IEnumerable<MembershipWebDetailsResponse>> GetAllMembershipsWithFeaturesAsync();
    Task<MembershipInfoResponse?> GetMembershipNameAsync(Guid membershipId);
}
