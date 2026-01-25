namespace GymManagementSystem.Core.DTO.ScheduledClass;
public class ScheduledClassResponse
{
    public Guid Id { get; set; }
    public string Date { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string StartFrom { get; set; } = default!;
    public string StartTo { get; set; } = default!;
    public int MaxPeople { get; set; }
    public string IsActive { get; set; } = default!;
    public string CreatedAt { get; set; } = default!;
    public string UpdatedAt { get; set; } = default!;
}
