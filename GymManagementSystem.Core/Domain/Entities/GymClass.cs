using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;

public class GymClass
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public Guid TrainerContractId { get; set; }
    public TrainerContract? Trainer { get; set; }
    public DaysOfWeekFlags DaysOfWeek { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan Duration { get; set; }
    public int MaxPeople { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    //public Guid? CreatedById { get; set; }
   // public Guid? UpdatedById { get; set; }
}
