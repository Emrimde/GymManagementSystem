using FluentValidation;
using GymManagementSystem.Core.DTO.Termination;

namespace GymManagementSystem.Core.Validators.Termination;
public class TerminationAddRequestValidator : AbstractValidator<TerminationAddRequest>
{
    public TerminationAddRequestValidator()
    {
        RuleFor(item => item.ClientId).NotEmpty().WithMessage("Client id is required");
    }
}
