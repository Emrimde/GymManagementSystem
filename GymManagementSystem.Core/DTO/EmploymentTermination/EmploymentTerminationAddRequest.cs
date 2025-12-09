namespace GymManagementSystem.Core.DTO.EmploymentTermination;
public class EmploymentTerminationAddRequest
{
    public Guid PersonId { get; set; }
    public DateTime EffectiveDate { get; set; }
}
