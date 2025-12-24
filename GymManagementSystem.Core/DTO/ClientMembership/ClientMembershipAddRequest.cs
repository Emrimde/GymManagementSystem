namespace GymManagementSystem.Core.DTO.ClientMembership;

public class ClientMembershipAddRequest
{
    public Guid ClientId { get; set; }
    public Guid MembershipId { get; set; }
}
