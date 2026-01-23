namespace GymManagementSystem.Core.DTO.MembershipFeature;
public class MembershipFeatureAddRequest
{
    public Guid MembershipId { get; set; }
    public string FeatureDescription { get; set; } = string.Empty;
}
