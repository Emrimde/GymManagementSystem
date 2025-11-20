namespace GymManagementSystem.Core.DTO.ClientMembership;

public class ClientMembershipAddRequest
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid MembershipId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
