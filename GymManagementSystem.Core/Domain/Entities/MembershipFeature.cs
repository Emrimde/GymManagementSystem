namespace GymManagementSystem.Core.Domain.Entities;
public class MembershipFeature
{
    public Guid Id { get; set; }
    public Guid MembershipId { get; set; }
    public Membership? Membership { get; set; }
    public string FeatureDescription { get; set; } = default!; 
}
