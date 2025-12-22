using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ScheduledClass;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IScheduledClassRepository : IRepository<ScheduledClassResponse, ScheduledClass>
{
    Task AddRangeAsync(IEnumerable<ScheduledClass> entities);
    Task<IEnumerable<ScheduledClassResponse>> GetAllScheduledClasses(Guid gymClassId, string? searchText = null);
    Task<IEnumerable<ScheduledClass>> GetAllScheduledClassesByGymClassId(Guid gymClassId);
}
