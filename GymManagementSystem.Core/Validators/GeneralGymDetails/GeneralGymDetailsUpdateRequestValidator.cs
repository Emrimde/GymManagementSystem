using FluentValidation;
using GymManagementSystem.Core.DTO.GeneralGymDetail;

namespace GymManagementSystem.Core.Validators.GeneralGymDetails;
public class GeneralGymDetailsUpdateRequestValidator : AbstractValidator<GeneralGymUpdateRequest>
{
    public GeneralGymDetailsUpdateRequestValidator()
    {
        RuleFor(item => item.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(item => item.GymName)
            .NotEmpty()
            .WithMessage("Gym name is required.");

        RuleFor(item => item.Address)
            .NotEmpty()
            .WithMessage("Address is required.");

        RuleFor(item => item.ContactNumber)
            .NotEmpty()
            .WithMessage("Contact number is required.");

        RuleFor(item => item.AboutUs)
            .NotEmpty()
            .WithMessage("About us section is required.");

        RuleFor(item => item.LogoUrl)
            .NotEmpty()
            .WithMessage("Logo URL is required.");
    }
}
