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
            Price = membership.Price,
            IsTrainerOnly = membership.IsTrainerOnly,
            MembershipType = membership.MembershipType,
            CanBeTerminated = membership.CanBeTerminated,
        };
    }
    public static Membership ToMembership(this MembershipAddRequest membership)
    {
        return new Membership()
        {
            Name = membership.Name,
            Price = membership.Price,
            IsTrainerOnly = membership.IsTrainerOnly,
            MembershipType = membership.MembershipType,
            CanBeTerminated = membership.CanBeTerminated,
        };
    }

    public static Membership ToMembership(this MembershipUpdateRequest membership)
    {
        return new Membership()
        {
            Name = membership.Name,
            Price = membership.Price,
            IsTrainerOnly = membership.IsTrainerOnly,
            MembershipType = membership.MembershipType ,
            CanBeTerminated = membership.CanBeTerminated,
        };
    }
}
