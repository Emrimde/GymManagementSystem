using FluentValidation;
using GymManagementSystem.Core.DTO.ClassBooking;

namespace GymManagementSystem.Core.Validators.ClassBooking;

public class ClassBookingAddRequestValidator : AbstractValidator<ClassBookingAddRequest>
{
    public ClassBookingAddRequestValidator()
    {
        RuleFor(item => item.ScheduledClassId)
            .NotEmpty().WithMessage("Scheduled class id is required.");
    }
}
