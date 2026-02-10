namespace GymManagementSystem.Core.DTO.TrainerTimeOff;
public class TrainerTimeOffAddRequest
{
    public Guid? TrainerId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public string? Reason { get; set; }
}
