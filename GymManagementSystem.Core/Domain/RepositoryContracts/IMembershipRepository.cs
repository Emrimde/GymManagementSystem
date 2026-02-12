using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.Membership;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IMembershipRepository : IRepository<MembershipResponse, Membership>
{
    public Task<IEnumerable<MembershipResponse>> GetAllMemberships();
    Task<IEnumerable<MembershipWebDetailsResponse>> GetAllMembershipsWithFeaturesAsync();
    Task<int> GetFreeFriendArrivalsPerMonthAsync(Guid membershipId);
    Task<MembershipInfoResponse?> GetMembershipNameAsync(Guid membershipId);
}
