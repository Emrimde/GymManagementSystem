using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IMembershipFeatureService
{
    Task<Result<Unit>> CreateMembershipFeatureAsync(MembershipFeatureAddRequest request);
    Task<Result<MembershipFeatureForEditResponse>> GetMembershipFeatureForEditAsync(Guid membershipId);
    Task<Result<IEnumerable<MembershipFeatureResponse>>> GetMembershipFeaturesByMembershipIdAsync(Guid membershipId);
    Task<Result<Unit>> HardDeleteMembershipFeatureAsync(Guid membershipFeatureId);
    Task<Result<Unit>> UpdateMembershipFeatureAsync(MembershipFeatureUpdateRequest entity);
}
