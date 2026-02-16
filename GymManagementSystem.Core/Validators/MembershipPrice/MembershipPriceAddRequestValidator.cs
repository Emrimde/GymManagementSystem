using FluentValidation;
using GymManagementSystem.Core.DTO.MembershipPrice;

namespace GymManagementSystem.Core.Validators.MembershipPrice;
public class MembershipPriceAddRequestValidator : AbstractValidator<MembershipPriceAddRequest>
{
    public MembershipPriceAddRequestValidator()
    {
        RuleFor(item => item.MembershipId).NotEmpty().WithMessage("Membership id is required");
        RuleFor(item => item.Price).GreaterThan(0).WithMessage("Price must be above 0");
    }
}
