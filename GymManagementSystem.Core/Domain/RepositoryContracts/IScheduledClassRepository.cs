using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IScheduledClassRepository : IRepository<ScheduledClassResponse, ScheduledClass>
{
    void AddRangeAsync(IEnumerable<ScheduledClass> entities);
    Task<IEnumerable<ScheduledClassResponse>> GetAllScheduledClasses(Guid gymClassId);
    Task<IEnumerable<ScheduledClass>> GetAllScheduledClassesByGymClassId(Guid gymClassId, int? classBookingDaysInAdvanceCount, bool showActive);
    Task<PageResult<ScheduledClassResponse>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null);
    Task<IEnumerable<ScheduledClass>> GetFutureUnbookedByGymClassId(Guid gymClassId);
    Task<IEnumerable<ScheduledClass>> GetAllScheduledClasses();
}
