namespace GymManagementSystem.Core.Domain.Entities;
public class EmploymentTermination
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public DateTime EffectiveDate { get; set; }      // kiedy kończy
    public Person? Person { get; set; }
    public DateTime RequestedDate { get; set; } = DateTime.UtcNow;     // kiedy uruchomiono wypowiedzenie
    public bool IsSigned { get; set; } = true;      
    public string? DocumentPath { get; set; }        
}
