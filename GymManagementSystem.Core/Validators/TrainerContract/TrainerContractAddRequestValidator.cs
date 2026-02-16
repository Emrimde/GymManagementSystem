using FluentValidation;
using GymManagementSystem.Core.DTO.TrainerContract;

namespace GymManagementSystem.Core.Validators.TrainerContract;
public class TrainerContractAddRequestValidator : AbstractValidator<TrainerContractAddRequest>
{
    public TrainerContractAddRequestValidator()
    {
        RuleFor(item => item.ContractType).IsInEnum().WithMessage("Invalid contract type");
        RuleFor(item => item.TrainerType).IsInEnum().WithMessage("Invalid trainer type.");
        RuleFor(item => item.PersonId).NotEmpty().WithMessage("Person id is required");
    }
}
