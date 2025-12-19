using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IMembershipFeatureService
{
    Task<Result<Unit>> CreateMembershipFeatureAsync(MembershipFeatureAddRequest request);
    Task<Result<IEnumerable<MembershipFeatureResponse>>> GetMembershipFeaturesByMembershipIdAsync(Guid membershipId);
}
