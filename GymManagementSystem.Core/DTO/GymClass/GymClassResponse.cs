using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.GymClass;
public class GymClassResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string StartHour { get; set; } = default!;
    public string Duration { get; set; } = default!;
    public string EndTime { get; set; } = default!;
    public string Days { get; set; } = default!;
    public int MaxPeople { get; set; }
    public string CreatedAt { get; set; } = default!;
    public string UpdatedAt { get; set; } = default!;
}
