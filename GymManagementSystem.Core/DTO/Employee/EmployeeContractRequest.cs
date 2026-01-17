using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Employee;
public class EmployeeContractRequest
{
    public Guid PersonId { get; set; }
    public decimal MonthlySalaryBrutto { get; set; }
    public EmployeeRole Role { get; set; }
    public EmploymentType EmploymentType { get; set; }
    public ContractTypeEnum ContractTypeEnum { get; set; }
}
