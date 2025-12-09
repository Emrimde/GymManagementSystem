namespace GymManagementSystem.Core.DTO.EmploymentTermination;
public class EmploymentTerminationGenerateResponse
{
    public DateTime RequestedDate { get; set; } 
    public DateTime EffectiveDate { get; set; }
    public string ContractType { get; set; } =default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? EmploymentType { get; set; }
}
