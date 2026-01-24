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

    public static void ModifyMembershipFeature(this MembershipFeature membershipFeature, MembershipFeatureUpdateRequest request)
    {
        membershipFeature.Id = request.MembershipFeatureId;
        membershipFeature.FeatureDescription = request.FeatureDescription;
    }

    public static MembershipFeatureResponse ToMembershipFeatureResponse(this MembershipFeature request)
    {
        return new MembershipFeatureResponse()
        {
            MembershipFeatureId = request.Id,
            FeatureDescription = request.FeatureDescription,
        };
    }
}
