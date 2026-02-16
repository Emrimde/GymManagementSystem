using FluentValidation;
using GymManagementSystem.Core.DTO.TrainerRate;

namespace GymManagementSystem.Core.Validators.TrainerRate;

public class TrainerRateAddRequestValidator :AbstractValidator<TrainerRateAddRequest>
{
    public TrainerRateAddRequestValidator()
    {
        RuleFor(item => item.DurationInMinutes).NotEmpty().WithMessage("Duration in minutes is required").GreaterThanOrEqualTo(60).WithMessage("Duration in minutes must be at least 60 ");
        RuleFor(item => item.TrainerContractId).NotEmpty().WithMessage("Trainer id is required");
        RuleFor(item => item.RatePerSessions).NotEmpty().WithMessage("Rate per session is required");
    }
}
