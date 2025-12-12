namespace GymManagementSystem.Core.Domain.Entities;

public class Termination
{
    public Guid Id { get; set; }
    public Guid ClientMembershipId { get; set; }
    public ClientMembership? ClientMembership { get; set; }
    public string? Reason { get; set; }
    public DateTime RequestedAt { get; set; }
    public string? IsSigned { get; set; }
}
