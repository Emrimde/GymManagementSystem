using FluentValidation;

namespace GymManagementSystem.Core.DTO.Auth;
public class SignInDtoValidator : AbstractValidator<SignInDto>
{
    public SignInDtoValidator()
    {
        RuleFor(item => item.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(item => item.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}
