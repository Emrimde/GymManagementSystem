using GymManagementSystem.API.Controllers;
using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class GymClassService : IGymClassService
{
    private readonly IGymClassRepository _gymClassRepo;
    private readonly IScheduledClassRepository _scheduledClassRepo;
    private readonly ITrainerRepository _trainerRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IScheduleGeneratorService _scheduleGeneratorService;
    private readonly IClassBookingRepository _classBookingRepository;
    public GymClassService(IGymClassRepository gymClassRepo, IScheduledClassRepository scheduledClassRepo, ITrainerRepository trainerRepo, IUnitOfWork unitOfWork, IScheduleGeneratorService scheduleGeneratorService, IClassBookingRepository classBookingRepository)
    {
        _gymClassRepo = gymClassRepo;
        _scheduledClassRepo = scheduledClassRepo;
        _trainerRepo = trainerRepo;
        _unitOfWork = unitOfWork;
        _scheduleGeneratorService = scheduleGeneratorService;
        _classBookingRepository = classBookingRepository;
    }

    public async Task<Result<GymClassInfoResponse>> CreateAsync(GymClassAddRequest request)
    {
        TrainerContract? trainer = await _trainerRepo.GetTrainerContractAsync(request.TrainerContractId, false);

        if (trainer == null)
        {
            return Result<GymClassInfoResponse>.Failure("Cannot check whether trainer is group instructor", StatusCodeEnum.InternalServerError);
        }

        if (trainer.TrainerType != TrainerTypeEnum.GroupInstructor)
        {
            return Result<GymClassInfoResponse>.Failure("The gym class cannot be saved because trainer isn't group instructor", StatusCodeEnum.BadRequest);
        }

        GymClass gymClass = request.ToGymClass();

        bool overlaps = await _gymClassRepo.ExistsOverlapAsync(gymClass);

        if (overlaps)
        {
            return Result<GymClassInfoResponse>.Failure("The gym class cannot be saved they are overlapping with other gym classes", StatusCodeEnum.BadRequest);
        }

         _gymClassRepo.CreateAsync(gymClass);

        List<ScheduledClass> scheduledClasses = _scheduleGeneratorService.GenerateScheduledClasses(gymClass);

         _scheduledClassRepo.AddRangeAsync(scheduledClasses);

        await _unitOfWork.SaveChangesAsync();

        return Result<GymClassInfoResponse>.Success(
            gymClass.ToGymInfoResponse(),
            StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> GenerateNewScheduledClassesAsync(Guid gymClassId)
    {
        GymClass? gymClass = await _gymClassRepo.GetByIdAsync(gymClassId);

        if (gymClass == null)
        {
            return Result<Unit>.Failure("Gym class not found", StatusCodeEnum.NotFound);
        }
        IEnumerable<ScheduledClass> presentScheduleClass = await _scheduledClassRepo.GetAllScheduledClassesByGymClassId(gymClassId, null, false);

        HashSet<DateTime> occupiedDates = presentScheduleClass.Select(item => item.Date).ToHashSet();

        List<ScheduledClass> scheduledClasses = _scheduleGeneratorService.GenerateScheduledClasses(gymClass, 14);
        List<ScheduledClass> newScheduledClasses = scheduledClasses.Where(item => !occupiedDates.Contains(item.Date)).ToList();

        _scheduledClassRepo.AddRangeAsync(newScheduledClasses);
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.NoContent);
    }

    public async Task<Result<IEnumerable<GymClassResponse>>> GetAllAsync(bool? isActive)
    {
        IEnumerable<GymClass> gymCLasses = await _gymClassRepo.GetAllAsync(isActive);
        IEnumerable<GymClassResponse> response = gymCLasses.Select(item => item.ToGymResponse());
        return Result<IEnumerable<GymClassResponse>>.Success(response, StatusCodeEnum.Ok);
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
        //gymClass.ModfiyGymClass(entity);


        IEnumerable<ScheduledClass> scheduledClasses = await _scheduledClassRepo.GetFutureUnbookedByGymClassId(entity.GymClassId);
        _scheduledClassRepo.DeleteScheduledClassList(scheduledClasses);

        List<ScheduledClass> scheduledClass = _scheduleGeneratorService.GenerateScheduledClasses(gymClass);

        _scheduledClassRepo.AddRangeAsync(scheduledClass);


        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.NoContent);
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

    public async Task<Result<Unit>> DeleteGymClassAsync(Guid gymClassId)
    {
        GymClass? gymClass = await _gymClassRepo.GetGymClassWithScheduledClassesAsync(gymClassId);

        if(gymClass == null)
        {
            return Result<Unit>.Failure("Gym class not found", StatusCodeEnum.NotFound);
        }

        
        _scheduledClassRepo.DeleteScheduledClassList(gymClass.ScheduledClasses);
        
        foreach(ScheduledClass scheduledClass in gymClass.ScheduledClasses)
        {
            _classBookingRepository.DeleteClassBookingList(scheduledClass.ClassBookings);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.NoContent);
    }

    public async Task<Result<Unit>> RestoreGymClassAsync(Guid gymClassId)
    {
        GymClass? gymClass = await _gymClassRepo.GetByIdAsync(gymClassId);
        if(gymClass == null || gymClass.IsActive == true)
        {
            return Result<Unit>.Failure("Gym class not found or already active", StatusCodeEnum.BadRequest);
        }
        TimeSpan endTime = gymClass.StartHour + gymClass.Duration;

        IEnumerable<GymClass> gymClasses = await _gymClassRepo.GetAllAsync(true);

        if (gymClasses.Any(item => (item.DaysOfWeek & gymClass.DaysOfWeek) != 0 && item.Id != gymClass.Id && endTime > item.StartHour && gymClass.StartHour < item.StartHour + item.Duration))
        {
            return Result<Unit>.Failure("The gym class cannot be saved they are overlapping with other gym classes", StatusCodeEnum.BadRequest);
        }

        //gymClass.IsActive = true;
        await _unitOfWork.SaveChangesAsync();

        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.NoContent);
    }
}
