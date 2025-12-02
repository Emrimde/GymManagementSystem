using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Employee;

public class EmployeeAddRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public decimal MonthlySalaryBrutto { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public EmployeeRole Role { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public ContractTypeEnum ContractTypeEnum { get; set; }
}
