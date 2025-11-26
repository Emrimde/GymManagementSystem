namespace GymManagementSystem.Core.Domain.Entities;

public class TrainerAvailabilityTemplate
{
    public Guid Id { get; set; }
    public Guid TrainerId { get; set; }
    public TimeSpan StartTime { get; set; } // np. 10:00
    public TimeSpan EndTime { get; set; }   // np. 20:00
    public int MinDurationMinutes { get; set; } = 60; // min 1h
    public int MaxDurationMinutes { get; set; } = 120; // max 2h
    public int IntervalMinutes { get; set; } = 15; // wybór co 15 min
    public Trainer? Trainer { get; set; }
}
