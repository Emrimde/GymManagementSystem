using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Membership;

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
            Price = membership.MembershipPrices != null ? membership.MembershipPrices.Where(item => item.ValidTo == null).Select(item => item.Price).FirstOrDefault() : 0m
        };
    }
    public static Membership ToMembership(this MembershipAddRequest membership)
    {
        return new Membership()
        {
            Name = membership.Name,
            //Price = membership.Price,
            MembershipType = membership.MembershipType,
        };
    }

    public static Membership ToMembership(this MembershipUpdateRequest membership)
    {
        return new Membership()
        {
            Name = membership.Name,
            //Price = membership.Price,
            MembershipType = membership.MembershipType,
        };
    }
}
