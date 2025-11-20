using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.GymClass;
public class GymClassResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public TimeSpan StartHour { get; set; }
    public TimeSpan Duration { get; set; }
    public string Days { get; set; }
    public int MaxPeople { get; set; }
    public decimal Price { get; set; }
    public string CreatedAt { get; set; } = default!;
    public string UpdatedAt { get; set; } = default!;
}
