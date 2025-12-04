namespace GymManagementSystem.Core.Domain.Entities;
public class TrainerTimeOff
{
    public Guid Id { get; set; }
    public Guid TrainerId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Reason { get; set; }
    public TrainerContract? Trainer { get; set; }
}

