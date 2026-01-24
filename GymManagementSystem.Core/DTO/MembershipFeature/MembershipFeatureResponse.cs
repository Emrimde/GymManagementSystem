namespace GymManagementSystem.Core.DTO.MembershipFeature;

public class MembershipFeatureResponse
{
    public Guid MembershipFeatureId { get; set; }
    public string FeatureDescription { get; set; } = default!;
}
