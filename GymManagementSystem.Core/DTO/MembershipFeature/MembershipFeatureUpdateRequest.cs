namespace GymManagementSystem.Core.DTO.MembershipFeature;
public class MembershipFeatureUpdateRequest
{
    public Guid MembershipFeatureId { get; set; }
    public string FeatureDescription { get; set; } = string.Empty;
}
