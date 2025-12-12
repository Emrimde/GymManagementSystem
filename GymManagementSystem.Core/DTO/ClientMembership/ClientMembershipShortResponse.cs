using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.ClientMembership;

public class ClientMembershipShortResponse
{
    public MembershipResponse? Membership { get; set; }
    public bool IsActive { get; set; }
    public MembershipStatusEnum MembershipStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid ContractId { get; set; }
}
