using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.GymClass;

public class GymClassUpdateRequest
{
    public Guid TrainerId { get; set; }
    public DaysOfWeekFlags DaysOfWeek { get; set; }
    public TimeSpan StartHour { get; set; }
    public TimeSpan Duration { get; set; }
    public int MaxPeople { get; set; }
}
