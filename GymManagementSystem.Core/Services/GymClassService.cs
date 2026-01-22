using GymManagementSystem.API.Controllers;
using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.Services;

public class GymClassService : IGymClassService
{
    private readonly IGymClassRepository _gymClassRepo;
    private readonly IScheduledClassRepository _scheduledClassRepo;
    private readonly ITrainerRepository _trainerRepo;
    private readonly IUnitOfWork _unitOfWork;
    public GymClassService(IGymClassRepository gymClassRepo, IScheduledClassRepository scheduledClassRepo, ITrainerRepository trainerRepo,IUnitOfWork unitOfWork)
    {
        _gymClassRepo = gymClassRepo;
        _scheduledClassRepo = scheduledClassRepo;
        _trainerRepo = trainerRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<GymClassInfoResponse>> CreateAsync(GymClassAddRequest entity)
    {
        TrainerContract? trainerContract = await _trainerRepo.GetTrainerContractAsync(entity.TrainerContractId,false);
        


        if(trainerContract == null)
        {
            return Result<GymClassInfoResponse>.Failure("Cannot check whether trainer is group instructor", StatusCodeEnum.InternalServerError);
        }

        if(trainerContract.TrainerType != TrainerTypeEnum.GroupInstructor)
        {
            return Result<GymClassInfoResponse>.Failure("The gym class cannot be saved because trainer isn't group instructor", StatusCodeEnum.BadRequest);
        }

        GymClass gymClass = entity.ToGymClass();
        gymClass.Duration = new TimeSpan(0, 59, 59);
        TimeSpan endTime = gymClass.StartHour + gymClass.Duration;

        List<GymClass> gymClasses = (List<GymClass>)await _gymClassRepo.GetAllAsync();

        if(gymClasses.Any(item => (item.DaysOfWeek & gymClass.DaysOfWeek) != 0 && endTime > item.StartHour && gymClass.StartHour < item.StartHour + item.Duration))
        {
            return Result<GymClassInfoResponse>.Failure("The gym class cannot be saved they are overlapping with other gym classes", StatusCodeEnum.BadRequest);
        }

        GymClass addedGymClass = await _gymClassRepo.CreateAsync(gymClass);
       List<ScheduledClass> scheduledClasses = GenerateScheduledClasses(addedGymClass);
       await _scheduledClassRepo.AddRangeAsync(scheduledClasses);
       return Result<GymClassInfoResponse>.Success(addedGymClass.ToGymInfoResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<GymClassResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<GymClass> gymCLasses = await _gymClassRepo.GetAllAsync();

        return Result<IEnumerable<GymClassResponse>>.Success(gymCLasses.Select(item => item.ToGymResponse()),StatusCodeEnum.Ok);
    }

    public Task<Result<GymClassDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<GymClassComboBoxResponse>>> GetGymClassesForSelectAsync()
    {
        IEnumerable<GymClassComboBoxResponse> dto = await _gymClassRepo.GetGymClassesForSelectAsync();
        return Result<IEnumerable<GymClassComboBoxResponse>>.Success(dto, StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> UpdateAsync(GymClassUpdateRequest entity)
    {
        GymClass? gymClass = await _gymClassRepo.GetByIdAsync(entity.GymClassId);
        if (gymClass == null)
        {
            return Result<Unit>.Failure("Gym class not found",StatusCodeEnum.NotFound);
        }
        gymClass.ModfiyGymClass(entity);


        IEnumerable<ScheduledClass> scheduledClasses = await _scheduledClassRepo.GetFutureUnbookedByGymClassId(entity.GymClassId);

        foreach (var item in scheduledClasses)
        {
            item.StartFrom = gymClass.StartHour;
            item.StartTo = gymClass.StartHour + gymClass.Duration;
            if (!IsDayIncluded(gymClass.DaysOfWeek, item.Date.DayOfWeek))
            {
                item.Date = GetNextValidDate(item.Date, gymClass.DaysOfWeek);
            }
        }

        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.NoContent);
    }

    private DateTime GetNextValidDate(DateTime date, DaysOfWeekFlags daysOfWeek)
    {
        for (int i = 0; i < 7; i++)
        {
            DateTime candidate = date.AddDays(i);
            if (IsDayIncluded(daysOfWeek, candidate.DayOfWeek))
                return candidate.Date;
        }

        throw new InvalidOperationException("No valid day in DaysOfWeekFlags");
    }

    public async Task<Result<Unit>> GenerateNewScheduledClassesAsync(Guid gymClassId)
    {
        GymClass? gymClass = await _gymClassRepo.GetByIdAsync(gymClassId);

        if (gymClass == null) {
            return Result<Unit>.Failure("Gym class not found", StatusCodeEnum.NotFound);
        }
        IEnumerable<ScheduledClass> presentScheduleClass = await _scheduledClassRepo.GetAllScheduledClassesByGymClassId(gymClassId, null);

        HashSet<DateTime> occupiedDates = presentScheduleClass.Select(item => item.Date).ToHashSet();

        List<ScheduledClass> scheduledClasses = GenerateScheduledClasses(gymClass,14);
        List<ScheduledClass> newScheduledClasses = scheduledClasses.Where(item => !occupiedDates.Contains(item.Date)).ToList();

        await _scheduledClassRepo.AddRangeAsync(newScheduledClasses);
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.NoContent);
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

    public async Task<Result<GymClassForEditResponse>> GetGymClassForEditAsync(Guid gymClassId)
    {
        GymClass? gymClass = await _gymClassRepo.GetByIdAsync(gymClassId);
        if (gymClass == null) 
        {
            return Result<GymClassForEditResponse>.Failure("Gym class not found", StatusCodeEnum.NotFound);
        }
        GymClassForEditResponse response = new GymClassForEditResponse()
        {
            DaysOfWeek = gymClass.DaysOfWeek,
            MaxPeople = gymClass.MaxPeople,
            Name = gymClass.Name,
            StartHour = gymClass.StartHour,
            TrainerContractId = gymClass.TrainerContractId
        };

        return Result<GymClassForEditResponse>.Success(response, StatusCodeEnum.Ok);
    }
}
