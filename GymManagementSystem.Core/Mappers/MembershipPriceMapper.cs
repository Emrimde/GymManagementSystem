using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.MembershipPrice;

namespace GymManagementSystem.Core.Mappers;

public static class MembershipPriceMapper
{
    public static MembershipPriceResponse ToMembershipPriceResponse(this MembershipPrice membershipPrice)
    {
        return new MembershipPriceResponse()
        {
            LabelPrice = membershipPrice.LabelPrice ?? "Regular",
            Price = membershipPrice.Price,
            ValidFromLabel = membershipPrice.ValidFrom.ToString("dd.MM.yyyy - HH:mm"),
            ValidToLabel = membershipPrice.ValidTo?.ToString("dd.MM.yyyy - HH:mm") ?? "Active"
        };
    }

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
