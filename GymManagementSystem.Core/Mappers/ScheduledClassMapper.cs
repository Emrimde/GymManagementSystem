using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ScheduledClass;

namespace GymManagementSystem.Core.Mappers;

public static class ScheduledClassMapper
{
    public static ScheduledClassResponse ToScheduledClassResponse(this ScheduledClass scheduledClass)
    {
        return new ScheduledClassResponse()
        {
            CreatedAt = scheduledClass.CreatedAt.ToString("dd.MM.yyyy"),
            UpdatedAt = scheduledClass.UpdatedAt.ToString("dd.MM.yyyy"),
            Date = scheduledClass.Date.ToString("dd.MM.yyyy"),
            Id = scheduledClass.Id,
            IsCancelled = scheduledClass.IsCancelled,
            MaxPeople = scheduledClass.MaxPeople,
            StartFrom = scheduledClass.StartFrom,
            StartTo = scheduledClass.StartTo,
        };
    }
}
