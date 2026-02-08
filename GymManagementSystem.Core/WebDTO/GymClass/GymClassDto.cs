using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.WebDTO.GymClass;
public class GymClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public TimeSpan StartHour { get; set; }
    public TimeSpan Duration { get; set; }
    public DaysOfWeekFlags DaysOfWeek { get; set; }
    public int MaxPeople { get; set; }
}
