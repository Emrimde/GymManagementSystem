using FluentValidation;
using GymManagementSystem.Core.DTO.Employee;

namespace GymManagementSystem.Core.Validators.Employee;
public class EmployeeAddRequestValidator : AbstractValidator<EmployeeAddRequest>
{
    public EmployeeAddRequestValidator()
    {
        RuleFor(item => item.EmploymentType).NotEmpty().WithMessage("Employment type is required");
        RuleFor(item => item.PersonId).NotEmpty().WithMessage("Perosn id is required");
        RuleFor(item => item.MonthlySalaryBrutto).NotEmpty().WithMessage("Employment type is required").GreaterThan(0).WithMessage("Monthly salary must be greater than 0");
        RuleFor(item => item.ContractTypeEnum).NotEmpty().WithMessage("Contract type  is required");
        RuleFor(item => item.Role).NotEmpty().WithMessage("Role is required");
    }
}
