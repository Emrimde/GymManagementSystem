using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;

public class Employee
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public decimal MonthlySalaryBrutto { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public EmployeeRole Role { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public ContractTypeEnum ContractTypeEnum { get; set; }
    public Person? Person { get; set; }
    public bool IsActive { get; set; } = true;
}
