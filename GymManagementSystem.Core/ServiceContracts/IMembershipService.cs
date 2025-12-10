
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IMembershipService
{
    Task<Result<MembershipResponse>> CreateAsync(MembershipAddRequest entity);
    Task<PageResult<MembershipResponse>> GetAllAsync();
    Task<Result<MembershipResponse>> GetByIdAsync(Guid id);
    Task<Result<MembershipResponse>> UpdateAsync(Guid id, MembershipUpdateRequest entity);
}
