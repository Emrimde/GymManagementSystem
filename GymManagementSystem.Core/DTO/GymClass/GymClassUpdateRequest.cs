using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.GymClass;

public class GymClassUpdateRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid GymClassId { get; set; }
    public Guid TrainerId { get; set; }
    public DaysOfWeekFlags DaysOfWeek { get; set; }
    public TimeSpan StartHour { get; set; }
    public int MaxPeople { get; set; }
}
