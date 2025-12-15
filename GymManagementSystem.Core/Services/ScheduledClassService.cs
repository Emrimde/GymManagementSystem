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
    public ScheduledClassService(IScheduledClassRepository schedulecClassRepo)
    {
        _schedulecClassRepo = schedulecClassRepo;
    }

    public async Task<Result<IEnumerable<ScheduledClassResponse>>> GetAllAsync(string? searchText)
    {
        IEnumerable<ScheduledClassResponse> scheduledClasses = await _schedulecClassRepo.GetAllScheduledClasses(searchText);
        return Result<IEnumerable<ScheduledClassResponse>>.Success(scheduledClasses, StatusCodeEnum.Ok);
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
