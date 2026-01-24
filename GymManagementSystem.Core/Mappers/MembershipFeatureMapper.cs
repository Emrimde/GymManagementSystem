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

    public static MembershipFeature ToMembershipFeature(this MembershipFeatureUpdateRequest request)
    {
        return new MembershipFeature()
        {
            MembershipId = request.MembershipId,
            FeatureDescription = request.FeatureDescription,
        };
    }

    public static void ModifyMembershipFeature(this MembershipFeature membershipFeature, MembershipFeatureUpdateRequest request)
    {
        membershipFeature.MembershipId = request.MembershipId;
        membershipFeature.FeatureDescription = request.FeatureDescription;
    }

    public static MembershipFeatureResponse ToMembershipFeatureResponse(this MembershipFeature request)
    {
        return new MembershipFeatureResponse()
        {
            FeatureDescription = request.FeatureDescription,
        };
    }
}
