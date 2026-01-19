using FluentValidation;
using GymManagementSystem.Core.DTO.Person;

namespace GymManagementSystem.Core.Validators.Person;
public class PersonAddRequestValidator : AbstractValidator<PersonAddRequest>
{
    public PersonAddRequestValidator()
    {
        RuleFor(item => item.FirstName)
          .NotEmpty().WithMessage("First name is required.")
          .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

        RuleFor(item => item.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

        RuleFor(item => item.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(60).WithMessage("Email cannot exceed 60 characters.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(item => item.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("A valid phone number is required.");

        RuleFor(item => item.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(60).WithMessage("Street cannot exceed 60 characters.");

        RuleFor(item => item.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City cannot exceed 50 characters.");
    }
}
