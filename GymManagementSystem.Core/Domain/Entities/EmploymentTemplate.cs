using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;
public class EmploymentTemplate
{
    public Guid Id { get; set; }
    public EmployeeRole Role { get; set; }
    public EmploymentType EmploymentType { get; set; } // dla bezpieczenstwa 
    public decimal? MonthlySalary { get; set; } // umowa o prace
    public decimal? HourlyRate {  get; set; } // zleceniowka
    public decimal? NetRate { get; set; } // B2B
    
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
