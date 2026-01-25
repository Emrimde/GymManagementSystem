using FluentValidation;
using GymManagementSystem.Core.DTO.Membership;

namespace GymManagementSystem.Core.Validators.Membership;
public class MembershipUpdateRequestValidator : AbstractValidator<MembershipUpdateRequest>
{
    public MembershipUpdateRequestValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(10).WithMessage("Name must have at least 10 characters.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

        RuleFor(item => item.ClassBookingDaysInAdvanceCount)
            .GreaterThanOrEqualTo(0).WithMessage("Value cannot be negative.");

        RuleFor(item => item.FreeFriendEntryCountPerMonth)
            .GreaterThanOrEqualTo(0).WithMessage("Value cannot be negative.");

        RuleFor(item => item.FreePersonalTrainingSessions)
            .GreaterThanOrEqualTo(0).WithMessage("Value cannot be negative.");
    }
}
