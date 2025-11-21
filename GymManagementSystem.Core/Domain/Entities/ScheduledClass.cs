namespace GymManagementSystem.Core.Domain.Entities;
public class ScheduledClass
{
    public  Guid Id { get; set; }
    public Guid GymClassId { get; set; }
    public GymClass GymClass { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartFrom { get; set; }
    public TimeSpan StartTo { get; set; }
    public int MaxPeople { get; set; }
    public bool IsCancelled { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    //public Guid? UpdatedById { get; set; }
}
