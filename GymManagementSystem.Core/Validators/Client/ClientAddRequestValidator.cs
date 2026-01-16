using FluentValidation;
using GymManagementSystem.Core.DTO.Client;

namespace GymManagementSystem.Core.Validators.Client;
public class ClientAddRequestValidator : AbstractValidator<ClientAddRequest>
{
    public ClientAddRequestValidator()
    {
        RuleFor(item => item.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");
        RuleFor(item => item.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
        RuleFor(item => item.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.")
            .MaximumLength(60).WithMessage("Email cannot exceed 100 characters.");
        RuleFor(item => item.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("A valid phone number is required.");
        RuleFor(item => item.DateOfBirth)
            .LessThanOrEqualTo(DateTime.UtcNow.AddYears(-13)).WithMessage("Client must be above 13");
        RuleFor(item => item.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(60).WithMessage("Street cannot exceed 60 characters.");
        RuleFor(item => item.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(60).WithMessage("City cannot exceed 50 characters.");
    }
}
