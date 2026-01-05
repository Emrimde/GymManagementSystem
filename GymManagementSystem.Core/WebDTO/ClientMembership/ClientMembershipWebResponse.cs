namespace GymManagementSystem.Core.WebDTO.ClientMembership;
public class ClientMembershipWebResponse
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Name { get; set; } 
    public bool IsActive { get; set; }
}
