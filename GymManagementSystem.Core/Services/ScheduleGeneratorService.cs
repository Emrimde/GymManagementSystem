using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ScheduleGeneratorService : IScheduleGeneratorService
{
    private readonly IGymClassRepository _gymClassRepo;
    private readonly IScheduledClassRepository _scheduledClassRepo;
    private readonly IUnitOfWork _unitOfWork;

    public ScheduleGeneratorService(IGymClassRepository gymClassRepo, IScheduledClassRepository scheduledClassRepository, IUnitOfWork unitOfWork)
    {
        _gymClassRepo = gymClassRepo;
        _scheduledClassRepo = scheduledClassRepository;
        _unitOfWork = unitOfWork;
    }

    //rolling window pattern
    public async Task GenerateNewScheduledClassesAsync()
    {
        DateTime today = DateTime.UtcNow.Date;
        DateTime horizon = today.AddDays(30);

        IEnumerable<GymClass> gymClasses = await _gymClassRepo.GetAllAsync(true);
        IEnumerable<ScheduledClass> scheduled = await _scheduledClassRepo.GetAllScheduledClasses();

        Dictionary<Guid, DateTime> grouped =
            scheduled
                .GroupBy(item => item.GymClassId)
                .ToDictionary(
                    group => group.Key,
                    group => group.Max(item => item.Date.Date)
                );

        List<ScheduledClass> allToGenerate = new();

        foreach (GymClass gymClass in gymClasses)
        {
            DateTime lastDate = grouped.TryGetValue(gymClass.Id, out DateTime maxDate)
                ? maxDate
                : today.AddDays(-1);

            if (lastDate >= horizon)
                continue;

            int daysToGenerate = (horizon - lastDate).Days;

            List<ScheduledClass> toGenerate =
                GenerateScheduledClasses(
                    gymClass,
                    daysToGenerate,
                    lastDate.AddDays(1)
                );

            allToGenerate.AddRange(toGenerate);
        }

        if (allToGenerate.Any())
        {
            _scheduledClassRepo.AddRangeAsync(allToGenerate);
            await _unitOfWork.SaveChangesAsync();
        }
    }



    public List<ScheduledClass> GenerateScheduledClasses(GymClass gymClass,int daysAhead = 30, DateTime? startDate = null)
    {
        List<ScheduledClass> result = new List<ScheduledClass>();
        DateTime start = startDate?.Date ?? DateTime.UtcNow.Date;

        for (int i = 0; i < daysAhead; i++)
        {
            DateTime date = start.AddDays(i).Date;
            if (!IsDayIncluded(gymClass.DaysOfWeek, date.DayOfWeek))
                continue;

            result.Add(new ScheduledClass
            {
                GymClassId = gymClass.Id,
                Date = date,
                StartFrom = gymClass.StartHour,
                StartTo = gymClass.StartHour + gymClass.Duration,
                MaxPeople = gymClass.MaxPeople,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });
        }

        return result;
    }
    public bool IsDayIncluded(DaysOfWeekFlags flags, DayOfWeek day)
    {
        DaysOfWeekFlags bit = day switch
        {
            DayOfWeek.Monday => DaysOfWeekFlags.Monday,
            DayOfWeek.Tuesday => DaysOfWeekFlags.Tuesday,
            DayOfWeek.Wednesday => DaysOfWeekFlags.Wednesday,
            DayOfWeek.Thursday => DaysOfWeekFlags.Thursday,
            DayOfWeek.Friday => DaysOfWeekFlags.Friday,
            DayOfWeek.Saturday => DaysOfWeekFlags.Saturday,
            DayOfWeek.Sunday => DaysOfWeekFlags.Sunday,
            _ => DaysOfWeekFlags.None
        };
        return (flags & bit) != 0;
    }
}
