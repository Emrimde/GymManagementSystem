using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.MembershipFeature;

namespace GymManagementSystem.Core.Mappers;
public static class MembershipFeatureMapper
{
    public static MembershipFeature ToMembershipFeature(this MembershipFeatureAddRequest request)
    {
        return new MembershipFeature()
        {
            FeatureDescription = request.FeatureDescription,
            MembershipId = request.MembershipId,
        };
    }
    public static MembershipFeatureResponse ToMembershipFeatureResponse(this MembershipFeature request)
    {
        return new MembershipFeatureResponse()
        {
            FeatureDescription = request.FeatureDescription,
        };
    }
}
