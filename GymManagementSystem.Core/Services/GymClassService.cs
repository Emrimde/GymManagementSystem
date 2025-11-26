using GymManagementSystem.API.Controllers;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.Services;

public class GymClassService : IGymClassService
{
    private readonly IRepository<GymClass> _gymClassRepo;
    private readonly IScheduledClassRepository _scheduledClassRepo;
    public GymClassService(IRepository<GymClass> gymClassRepo, IScheduledClassRepository scheduledClassRepo)
    {
        _gymClassRepo = gymClassRepo;
        _scheduledClassRepo = scheduledClassRepo;
    }

    public async Task<Result<GymClassInfoResponse>> CreateAsync(GymClassAddRequest entity)
    {
       GymClass gymClass = await _gymClassRepo.CreateAsync(entity.ToGymClass());
       List<ScheduledClass> scheduledClasses = GenerateScheduledClasses(gymClass);
       await _scheduledClassRepo.AddRangeAsync(scheduledClasses);
       return Result<GymClassInfoResponse>.Success(gymClass.ToGymInfoResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<GymClassResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<GymClass> gymCLasses = await _gymClassRepo.GetAllAsync(cancellationToken);

        return Result<IEnumerable<GymClassResponse>>.Success(gymCLasses.Select(item => item.ToGymResponse()),StatusCodeEnum.Ok);
    }

    public Task<Result<GymClassDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<GymClassInfoResponse>> UpdateAsync(Guid id, GymClassUpdateRequest entity)
    {
        throw new NotImplementedException();
    }

    private List<ScheduledClass> GenerateScheduledClasses(GymClass gymClass, int daysAhead = 30)
    {
        List<ScheduledClass> result = new List<ScheduledClass>();
        DateTime today = DateTime.UtcNow.Date;

        for (int i = 0; i < daysAhead; i++)
        {
            DateTime date = today.AddDays(i);
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


    private bool IsDayIncluded(DaysOfWeekFlags flags, DayOfWeek day)
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
