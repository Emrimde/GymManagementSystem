using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.MembershipFeature;
public class MembershipFeatureAddRequest
{
    public Guid MembershipId { get; set; }
    public Guid FeatureId { get; set; }
    public int? Frequency { get; set; }
    public PeriodEnum Period { get; set; }
}
