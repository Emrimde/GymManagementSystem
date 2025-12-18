using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IMembershipPriceService
{
    Task<Result<Unit>> CreateMembershipPriceAsync(MembershipPriceAddRequest membershipPriceAddRequest);
    Task<Result<IEnumerable<MembershipPriceResponse>>> GetMembershipPricesByMembershipIdAsync(Guid membershipId);
}
