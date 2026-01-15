using FluentValidation;
using GymManagementSystem.Core.DTO.Client;

namespace GymManagementSystem.Core.Validators.Client
{
    public class ClientUpdateRequestValidator : AbstractValidator<ClientUpdateRequest>
    {
        public ClientUpdateRequestValidator()
        {
            RuleFor(item => item.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");
            RuleFor(item => item.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("A valid phone number is required.")
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 characters.");
            RuleFor(item => item.Street).NotEmpty().WithMessage("Street is required.")
                .MaximumLength(100).WithMessage("Street cannot exceed 100 characters.");
            RuleFor(item => item.City).NotEmpty().WithMessage("City is required.").MaximumLength(100).WithMessage("City cannot exceed 100 characters.");
        }
    }
}
