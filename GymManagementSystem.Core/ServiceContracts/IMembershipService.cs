using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.Membership;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IMembershipService
{
    Task<Result<MembershipResponse>> CreateAsync(MembershipAddRequest entity);
    Task<Result<IEnumerable<MembershipResponse>>> GetAllAsync();
    Task<Result<MembershipResponse>> GetByIdAsync(Guid membershipId);
    Task<Result<MembershipInfoResponse>> GetMembershipNameAsync(Guid membershipId);
    Task<Result<MembershipResponse>> UpdateAsync(Guid id, MembershipUpdateRequest entity);
    Task<Result<IEnumerable<MembershipWebDetailsResponse>>> GetAllMembershipsWithFeaturesAsync();
}
