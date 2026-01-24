using FluentValidation;
using GymManagementSystem.Core.DTO.Membership;

namespace GymManagementSystem.Core.Validators.Membership;
public class MembershipUpdateRequestValidator : AbstractValidator<MembershipUpdateRequest>
{
    public MembershipUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(10).WithMessage("Name must have at least 10 characters.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(x => x.ClassBookingDaysInAdvanceCount)
            .GreaterThanOrEqualTo(0).WithMessage("Value cannot be negative.");

        RuleFor(x => x.FreeFriendEntryCountPerMonth)
            .GreaterThanOrEqualTo(0).WithMessage("Value cannot be negative.");

        RuleFor(x => x.FreePersonalTrainingSessions)
            .GreaterThanOrEqualTo(0).WithMessage("Value cannot be negative.");
    }
}
