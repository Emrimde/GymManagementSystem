namespace GymManagementSystem.Core.Domain.Entities;
public class ClassBooking
{
    public Guid Id { get; set; }
    public Guid ScheduledClassId { get; set; }
    public ScheduledClass? ScheduledClass { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CancelledAt { get; set; } 
    public bool IsActive { get; set; } = true;
}
