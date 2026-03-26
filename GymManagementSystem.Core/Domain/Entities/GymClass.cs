using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;

public class GymClass
{
    private static readonly TimeSpan DurationConst = TimeSpan.FromMinutes(60);

    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public Guid TrainerContractId { get; private set; }
    public TrainerContract? Trainer { get; private set; }
    public DaysOfWeekFlags DaysOfWeek { get; private set; }
    public TimeSpan StartHour { get; private set; }
    public int MaxPeople { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    public ICollection<ScheduledClass> ScheduledClasses { get; private set; } = new List<ScheduledClass>();

    public TimeSpan Duration => DurationConst;
    public TimeSpan EndHour => StartHour + DurationConst;

    private GymClass() { }

    public GymClass(string name, Guid trainerContractId, DaysOfWeekFlags daysOfWeek, TimeSpan startHour, int maxPeople)
    {
        Name = name;
        TrainerContractId = trainerContractId;
        DaysOfWeek = daysOfWeek;
        StartHour = startHour;
        MaxPeople = maxPeople;
    }

    public void Update(string name, DaysOfWeekFlags daysOfWeek, TimeSpan startHour, Guid trainerId, int maxPeople)
    {
        if (maxPeople <= 0)
        {
            throw new Exception("Invalid max people");
        }

        Name = name;
        DaysOfWeek = daysOfWeek;
        StartHour = startHour;
        TrainerContractId = trainerId;
        MaxPeople = maxPeople;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

}
