using FluentValidation;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Validators.GymClass;
public class GymClassAddRequestValidator : AbstractValidator<GymClassAddRequest>
{
    public GymClassAddRequestValidator()
    {
        RuleFor(item => item.Name)
           .NotEmpty()
           .WithMessage("Name is required.");

        RuleFor(item => item.TrainerContractId)
            .NotEmpty()
            .WithMessage("Trainer must be selected.");

        RuleFor(item => item.DaysOfWeek)
            .NotEqual(DaysOfWeekFlags.None)
            .WithMessage("Select at least one day.");

        RuleFor(item => item.StartHour)
            .NotEqual(default(TimeSpan))
            .WithMessage("Start hour must be set.")
            .Must(item => item >= TimeSpan.FromHours(7) && item <= TimeSpan.FromHours(22))
            .WithMessage("Start hour must be between 7:00 and 22:00.");

        RuleFor(item => item.MaxPeople)
            .GreaterThan(0)
            .WithMessage("Max people must be greater than 0.");
    }
}
