using FluentValidation;
using GymManagementSystem.Core.DTO.Auth;

public class ChangePasswordRequestValidator
    : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(item => item.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required.")
            .MinimumLength(5).WithMessage("Current password is too short.");

        RuleFor(item => item.NewPassword)
            .NotEmpty().WithMessage("New password is required.")
            .MinimumLength(5).WithMessage("New password is too short.")
            .NotEqual(item => item.CurrentPassword)
            .WithMessage("New password must be different.");
    }
}
