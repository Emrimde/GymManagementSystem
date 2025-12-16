namespace GymManagementSystem.Core.DTO.ClientMembership;

public class ClientMembershipResponse
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public string Name { get; set; } = default!;
    public string MembershipType { get; set; } = default!;
    public bool IsActive { get; set; }
    public string StartDate { get; set; } = default!;
    public string? EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
