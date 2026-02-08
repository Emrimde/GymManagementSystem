namespace GymManagementSystem.Core.WebDTO.ScheduledClassDto;
public class ScheduledClassDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartFrom { get; set; }
    public TimeSpan StartTo { get; set; }
    public int MaxPeople { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsActive { get; set; }
}
