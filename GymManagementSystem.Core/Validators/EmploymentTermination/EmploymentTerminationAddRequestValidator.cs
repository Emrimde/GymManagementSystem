using FluentValidation;
using GymManagementSystem.Core.DTO.EmploymentTermination;

namespace GymManagementSystem.Core.Validators.EmploymentTermination;

public class EmploymentTerminationAddRequestValidator : AbstractValidator<EmploymentTerminationAddRequest>
{
    public EmploymentTerminationAddRequestValidator()
    {
               RuleFor(item => item.PersonId)
            .NotEmpty().WithMessage("Person id is required.");
    }
}
