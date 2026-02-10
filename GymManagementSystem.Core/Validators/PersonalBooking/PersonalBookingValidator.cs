using FluentValidation;
using GymManagementSystem.Core.DTO.PersonalBooking;

namespace GymManagementSystem.Core.Validators.PersonalBooking;
public class PersonalBookingValidator : AbstractValidator<PersonalBookingAddRequest>
{
    public PersonalBookingValidator()
    {
        RuleFor(item => item.StartDay.Date).GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("This day has passed").NotEmpty().WithMessage("Start day is required");
        RuleFor(item => item.TrainerRateId).NotEmpty().WithMessage("Trainer rate id is required");
        RuleFor(item => item.TrainerId).NotEmpty().WithMessage("Trainer id is required");
    }
}
