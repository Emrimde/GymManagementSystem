using FluentValidation;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Validators.Employee;
public class EmployeeAddRequestValidator : AbstractValidator<EmployeeAddRequest>
{
    public EmployeeAddRequestValidator()
    {
        RuleFor(x => x.EmploymentType).NotEqual(EmploymentType.None).WithMessage("Employment type is required");
        RuleFor(item => item.PersonId).NotEmpty().WithMessage("Perosn id is required");
        RuleFor(item => item.MonthlySalaryBrutto).NotEmpty().WithMessage("Employment type is required").GreaterThan(0).WithMessage("Monthly salary must be greater than 0");
        RuleFor(item => item.Role).NotEqual(EmployeeRole.None).WithMessage("Role is required");
    }
}

