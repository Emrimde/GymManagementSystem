using FluentValidation;
using GymManagementSystem.Core.DTO.TrainerTimeOff;

namespace GymManagementSystem.Core.Validators.TrainerTimeOff;
public class TrainerTimeOffAddRequestValidator : AbstractValidator<TrainerTimeOffAddRequest>
{
    public TrainerTimeOffAddRequestValidator()
    {
        RuleFor(item => item.Start)
            .LessThan(item => item.End)
            .WithMessage("End time must be later than start time");

        RuleFor(item => item.Start)
            .GreaterThanOrEqualTo(item => DateTime.UtcNow.AddMinutes(-1))
            .WithMessage("You cannot set time off for the past");

        RuleFor(item => item.TrainerId)
            .NotEmpty()
            .WithMessage("Trainer identifier is required");
    }
}
