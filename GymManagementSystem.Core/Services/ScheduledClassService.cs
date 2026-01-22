using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClassBooking;
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

    public ScheduledClassService(IScheduledClassRepository schedulecClassRepo, IClassBookingRepository classBookingRepo, IMembershipRepository memberhsipRepository)
    {
        _schedulecClassRepo = schedulecClassRepo;
        _classBookingRepo = classBookingRepo;
        _memberhsipRepository = memberhsipRepository;
    }
        

    public async Task<Result<IEnumerable<ScheduledClassResponse>>> GetAllAsync(Guid gymClassId, string? searchText)
    {
        IEnumerable<ScheduledClassResponse> scheduledClasses = await _schedulecClassRepo.GetAllScheduledClasses(gymClassId,searchText);
        return Result<IEnumerable<ScheduledClassResponse>>.Success(scheduledClasses, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<ScheduledClassComboBoxResponse>>> GetAllScheduledClassesByGymClassId(Guid gymclassId, Guid membershipId)
    {
        Membership? membership = await _memberhsipRepository.GetByIdAsync(membershipId);
        if (membership == null)
        {
            return Result<IEnumerable<ScheduledClassComboBoxResponse>>.Failure("Membership not found", StatusCodeEnum.NotFound);
        }

        IEnumerable<ScheduledClass> scheduledClasses = await _schedulecClassRepo.GetAllScheduledClassesByGymClassId(gymclassId, membership.ClassBookingDaysInAdvanceCount);
        IEnumerable<ScheduledClassComboBoxResponse> dto = scheduledClasses.OrderBy(item => item.Date).Select(item => item.ToScheduledClassComboBoxResponse());
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
