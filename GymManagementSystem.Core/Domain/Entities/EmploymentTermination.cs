namespace GymManagementSystem.Core.Domain.Entities;
public class EmploymentTermination
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public DateTime EffectiveDate { get; set; }    
    public Person? Person { get; set; }
    public DateTime RequestedDate { get; set; } = DateTime.UtcNow; 
    public bool IsSigned { get; set; } = true;      
}
