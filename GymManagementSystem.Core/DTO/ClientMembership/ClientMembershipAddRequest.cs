namespace GymManagementSystem.Core.DTO.ClientMembership;

public class ClientMembershipAddRequest
{
    public Guid ClientId { get; set; }
    public Guid MembershipId { get; set; }
    // Indicates if the request is made from the web/mobile application
    public bool IsFromWeb { get; set; } = false;
}
