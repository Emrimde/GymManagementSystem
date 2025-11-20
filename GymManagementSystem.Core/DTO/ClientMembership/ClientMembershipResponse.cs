using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Membership;

namespace GymManagementSystem.Core.DTO.ClientMembership;

public class ClientMembershipResponse
{
    public Guid Id { get; set; }
    public ClientResponse? Client { get; set; }
    public MembershipResponse? Membership { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}
