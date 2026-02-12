using FluentValidation;
using GymManagementSystem.Core.DTO.Client;

namespace GymManagementSystem.Core.Validators.Client;
public class ClientUpdateRequestValidator : AbstractValidator<ClientUpdateRequest>
{
    public ClientUpdateRequestValidator()
    {
        RuleFor(item => item.LastName)
       .NotEmpty().WithMessage("Last name is required.")
       .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.")
       .Matches(@"^[^\d]+$")
           .WithMessage("Last name cannot contain numbers.")
       .Matches(@".*[A-Z].*")
           .WithMessage("Last name must contain at least one uppercase letter.");

        RuleFor(item => item.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{9,}$")
                .WithMessage("Phone number must contain at least 9 digits and digits only.");

        RuleFor(item => item.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(60).WithMessage("Street cannot exceed 60 characters.")
            .Matches(@".*[A-Z].*")
                .WithMessage("Street must contain at least one uppercase letter.");

        RuleFor(item => item.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(50).WithMessage("City cannot exceed 50 characters.")
            .Matches(@".*[A-Z].*")
                .WithMessage("City must contain at least one uppercase letter.");
    }
}
