namespace GymManagementSystem.Core.Domain.Entities;
public class MembershipPrice
{
    public Guid Id { get; set; }
    public Guid MembershipId { get; set; }
    public Membership Membership { get; set; } = null!;
    public decimal Price { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo  { get; set; }
    public string? LabelPrice { get; set; }
}
