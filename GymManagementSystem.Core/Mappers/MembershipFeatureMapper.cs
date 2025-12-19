using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.MembershipFeature;

namespace GymManagementSystem.Core.Mappers;
public static class MembershipFeatureMapper
{
    public static MembershipFeature ToMembershipFeature(this MembershipFeatureAddRequest request)
    {
        return new MembershipFeature()
        {
            BenefitFrequency = request.Frequency,
            FeatureId = request.FeatureId,
            MembershipId = request.MembershipId,
            Period = request.Period,
        };
    }
    public static MembershipFeatureResponse ToMembershipFeatureResponse(this MembershipFeature request)
    {
        return new MembershipFeatureResponse()
        {
            BenefitFrequency = request.BenefitFrequency.ToString() ?? "Not set",
            BenefitDesciption = request.Feature!.BenefitDescription,
            Period = request.Period.ToString() ?? "Not set",
        };
    }
}
