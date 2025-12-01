using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.EmploymentTemplate;
public class EmploymentTemplateAddRequest
{
    public EmployeeRole Role { get; set; }
    public EmploymentType EmploymentType { get; set; } 
    public decimal? MonthlySalary { get; set; } 
    public decimal? HourlyRate { get; set; }
    public decimal? NetRate { get; set; } 
}
