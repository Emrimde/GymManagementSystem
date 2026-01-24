using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.MembershipPrice;

namespace GymManagementSystem.Core.Mappers;

public static class MembershipPriceMapper
{
    public static MembershipPrice ToMembershipPrice(this MembershipPriceAddRequest request)
    {
        return new MembershipPrice()
        {
            MembershipId = request.MembershipId,
            LabelPrice = request.LabelPrice,
            Price = request.Price,
        };
    }
}
