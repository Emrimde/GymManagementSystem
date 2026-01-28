using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IScheduleGeneratorService
{
    Task GenerateNewScheduledClassesAsync();
    List<ScheduledClass> GenerateScheduledClasses(GymClass gymClass, int daysAhead = 30, DateTime? startDate = null);
    bool IsDayIncluded(DaysOfWeekFlags flags, DayOfWeek day);
}
