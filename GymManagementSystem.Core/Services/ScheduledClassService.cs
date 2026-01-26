using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ScheduledClassService : IScheduledClassService
{
    private readonly IScheduledClassRepository _schedulecClassRepo;
    private readonly IClassBookingRepository _classBookingRepo;
    private readonly IMembershipRepository _memberhsipRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ScheduledClassService(IScheduledClassRepository schedulecClassRepo, IClassBookingRepository classBookingRepo, IMembershipRepository memberhsipRepository, IUnitOfWork unitOfWork)
    {
        _schedulecClassRepo = schedulecClassRepo;
        _classBookingRepo = classBookingRepo;
        _memberhsipRepository = memberhsipRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> CancelScheduleClassAsync(Guid scheduleClassId)
    {
        ScheduledClass? scheduleClass = await _schedulecClassRepo.GetByIdAsync(scheduleClassId);
        if(scheduleClass == null)
        {
            return Result<Unit>.Failure("Scheduled class not found", StatusCodeEnum.NotFound);
        }
        scheduleClass.IsActive = false;
        foreach(ClassBooking classBooking in scheduleClass.ClassBookings)
        {
            classBooking.IsActive = false;
        }

        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.NoContent);
    }


    public async Task<Result<IEnumerable<ScheduledClassResponse>>> GetAllAsync(Guid gymClassId, string? searchText)
    {
        IEnumerable<ScheduledClassResponse> scheduledClasses = await _schedulecClassRepo.GetAllScheduledClasses(gymClassId,searchText);
        return Result<IEnumerable<ScheduledClassResponse>>.Success(scheduledClasses, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<ScheduledClassComboBoxResponse>>> GetAllScheduledClassesByGymClassId(Guid gymclassId, Guid membershipId,Guid clientId)
    {
        Membership? membership = await _memberhsipRepository.GetByIdAsync(membershipId);
        if (membership == null)
        {
            return Result<IEnumerable<ScheduledClassComboBoxResponse>>.Failure("Membership not found", StatusCodeEnum.NotFound);
        }

        IEnumerable<ScheduledClass> scheduledClasses = await _schedulecClassRepo.GetAllScheduledClassesByGymClassId(gymclassId, membership.ClassBookingDaysInAdvanceCount, true);

        IEnumerable<ScheduledClass> scheduledClassesToDisplay = scheduledClasses.Where(item => !item.ClassBookings.Any(item => item.ClientId == clientId)).ToList();

        IEnumerable<ScheduledClassComboBoxResponse> dto = scheduledClassesToDisplay.OrderBy(item => item.Date).Select(item => item.ToScheduledClassComboBoxResponse());
        return Result<IEnumerable<ScheduledClassComboBoxResponse>>.Success(dto, StatusCodeEnum.Ok);
    }

    public async Task<Result<ScheduledClassDetailsResponse>> GetByIdAsync(Guid id)
    {
       ScheduledClass? scheduledClass = await _schedulecClassRepo.GetByIdAsync(id);
        if (scheduledClass == null) 
        {
            return Result<ScheduledClassDetailsResponse>.Failure("Scheduled class not found", StatusCodeEnum.NotFound);
        }

        return Result<ScheduledClassDetailsResponse>.Success(scheduledClass.ToScheduledClassDetailsResponse(), StatusCodeEnum.Ok);
    }
}
