using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.MembershipFeature;

public class MembershipFeatureResponse
{
    public string BenefitDesciption { get; set; } = default!;
    public string BenefitFrequency { get; set; } = default!;
    public string Period { get; set; } = default!;
}
