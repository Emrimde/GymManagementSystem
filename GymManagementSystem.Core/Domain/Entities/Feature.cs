namespace GymManagementSystem.Core.Domain.Entities;
public class Feature
{
    public Guid Id { get; set; }
    public string BenefitDescription { get; set; } = default!;
    public ICollection<MembershipFeature> MembershipFeatures { get; set; } = new List<MembershipFeature>();
}
