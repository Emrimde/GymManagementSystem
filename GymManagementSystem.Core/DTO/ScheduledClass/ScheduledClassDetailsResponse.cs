namespace GymManagementSystem.Core.DTO.ScheduledClass;

public class ScheduledClassDetailsResponse
{
    public Guid Id { get; set; }
    public Guid GymClassId { get; set; }
    public string Date { get; set; } = default!;
    public TimeSpan StartFrom { get; set; }
    public TimeSpan StartTo { get; set; }
    public int AttendeesCount { get; set; }
    public int MaxPeople { get; set; }
    public bool IsCancelled { get; set; } = false;
    public string CreatedAt { get; set; } = default!;
    public string UpdatedAt { get; set; } = default!;
}
