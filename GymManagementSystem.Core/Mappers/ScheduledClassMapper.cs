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
            Name = scheduledClass.GymClass?.Name ?? string.Empty,
            Id = scheduledClass.Id,
            IsCancelled = scheduledClass.IsCancelled,
            MaxPeople = scheduledClass.MaxPeople,
            StartFrom = scheduledClass.StartFrom,
            StartTo = scheduledClass.StartTo,
        };
    }
    public static ScheduledClassComboBoxResponse ToScheduledClassComboBoxResponse(this ScheduledClass scheduledClass)
    {
        var end = scheduledClass.StartFrom + TimeSpan.FromHours(1);
        return new ScheduledClassComboBoxResponse()
        {
            ScheduledClassId = scheduledClass.Id,
            ScheduledClassDetails = scheduledClass.Date.ToString("dd.MM") + " - " +  scheduledClass.StartFrom.ToString(@"hh\:mm") + " - " + end.ToString(@"hh\:mm") + " - Slots " +  scheduledClass.ClassBookings.Count.ToString() + " / " + scheduledClass.GymClass!.MaxPeople.ToString()
        };
    }

    public static ScheduledClassDetailsResponse ToScheduledClassDetailsResponse(this ScheduledClass scheduledClass)
    {
        return new ScheduledClassDetailsResponse()
        {
            CreatedAt = scheduledClass.CreatedAt.ToString("dd.MM.yyyy"),
            UpdatedAt = scheduledClass.UpdatedAt.ToString("dd.MM.yyyy"),
            Date = scheduledClass.Date.ToString("dd.MM.yyyy"),
            AttendeesCount = scheduledClass.ClassBookings.Count,
            Id = scheduledClass.Id,
            IsCancelled = scheduledClass.IsCancelled,
            MaxPeople = scheduledClass.MaxPeople,
            StartFrom = scheduledClass.StartFrom,
            StartTo = scheduledClass.StartTo,
        };
    }
}
