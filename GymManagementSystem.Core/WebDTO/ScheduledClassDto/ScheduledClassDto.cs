namespace GymManagementSystem.Core.WebDTO.ScheduledClassDto;
public class ScheduledClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Date { get; set; } = default!;
    public string StartFrom { get; set; } = default!;
    public string StartTo { get; set; } = default!;
    public int MaxPeople { get; set; }
    public bool IsCancelled { get; set; }
    public bool IsActive { get; set; }
}
