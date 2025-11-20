namespace GymManagementSystem.Core.Domain.Entities;

public class ClientMembership
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public Guid MembershipId { get; set; }
    public Membership? Membership { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Contract? Contract { get; set; }
}
    // contract bedzie mial ClientMembershipId  kilka umow bo co jesli bedzie chcial zmienic
