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

    public async Task<Result<IEnumerable<ScheduledClassResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<ScheduledClass> scheduledClasses = await _schedulecClassRepo.GetAllAsync(cancellationToken);
        return Result<IEnumerable<ScheduledClassResponse>>.Success(scheduledClasses.Select(item => item.ToScheduledClassResponse()), StatusCodeEnum.Ok);
    }
}
