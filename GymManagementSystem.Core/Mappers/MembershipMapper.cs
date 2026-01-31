using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Membership;
using System.Globalization;

namespace GymManagementSystem.Core.Mappers;

public static class MembershipMapper
{
    public static MembershipResponse ToMembershipResponse(this Membership membership)
    {
        return new MembershipResponse()
        {
            Id = membership.Id,
            Name = membership.Name,
            MembershipType = membership.MembershipType,
            Price = membership.MembershipPrices?.Where(item => item.ValidTo == null).Select(item => item.Price.ToString("0.0",CultureInfo.InvariantCulture)).FirstOrDefault() ?? "0.0" ,
            ClassBookingDaysInAdvanceCount = membership.ClassBookingDaysInAdvanceCount,
            FreeFriendEntryCountPerMonth = membership.FreeFriendEntryCountPerMonth,
            FreePersonalTrainingSessions = membership.FreePersonalTrainingSessions
        };
    }
    public static Membership ToMembership(this MembershipAddRequest membership)
    {
        return new Membership()
        {
            Name = membership.Name,
            MembershipType = membership.MembershipType,
        };
    }

    public static void ModifyMembership(this Membership membership, MembershipUpdateRequest membershipUpdateRequest)
    {
        membership.Name = membershipUpdateRequest.Name;
        membership.FreeFriendEntryCountPerMonth = membershipUpdateRequest.FreeFriendEntryCountPerMonth;
        membership.FreePersonalTrainingSessions = membershipUpdateRequest.FreePersonalTrainingSessions;
        membership.ClassBookingDaysInAdvanceCount = membershipUpdateRequest.ClassBookingDaysInAdvanceCount;
    }
}
