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
}
