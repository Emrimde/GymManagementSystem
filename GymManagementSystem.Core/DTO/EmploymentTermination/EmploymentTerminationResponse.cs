namespace GymManagementSystem.Core.DTO.EmploymentTermination;

public class EmploymentTerminationResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime RequestedDate { get; set; }
    public DateTime EffectiveDate { get; set; }
}
