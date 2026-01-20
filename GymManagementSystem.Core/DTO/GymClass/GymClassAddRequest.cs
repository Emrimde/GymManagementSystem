using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.GymClass;
public class GymClassAddRequest
{
    public string Name { get; set; } = default!;
    public Guid TrainerContractId { get; set; }
    public DaysOfWeekFlags DaysOfWeek { get; set; }
    public TimeSpan StartHour { get; set; }
    public int MaxPeople { get; set; }
}
