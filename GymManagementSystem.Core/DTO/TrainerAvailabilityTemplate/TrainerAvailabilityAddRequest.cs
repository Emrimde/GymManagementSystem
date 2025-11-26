namespace GymManagementSystem.Core.DTO.TrainerAvailabilityTemplate;
public class TrainerAvailabilityAddRequest
{
    public Guid TrainerId { get; set; }
    public TimeSpan StartTime { get; set; } 
    public TimeSpan EndTime { get; set; }   
}
