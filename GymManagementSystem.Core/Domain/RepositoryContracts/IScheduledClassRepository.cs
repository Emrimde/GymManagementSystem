using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IScheduledClassRepository : IRepository<ScheduledClassResponse, ScheduledClass>
{
    Task AddRangeAsync(IEnumerable<ScheduledClass> entities);
    Task<IEnumerable<ScheduledClassResponse>> GetAllScheduledClasses(Guid gymClassId, string? searchText = null);
    Task<IEnumerable<ScheduledClass>> GetAllScheduledClassesByGymClassId(Guid gymClassId, int? classBookingDaysInAdvanceCount);
    Task<PageResult<ScheduledClassResponse>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null);
    Task<IEnumerable<ScheduledClass>> GetFutureUnbookedByGymClassId(Guid gymClassId);
}
