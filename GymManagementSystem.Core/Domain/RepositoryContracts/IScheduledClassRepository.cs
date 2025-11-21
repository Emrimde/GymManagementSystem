using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IScheduledClassRepository : IRepository<ScheduledClass>
{
    Task AddRangeAsync(IEnumerable<ScheduledClass> entities, CancellationToken cancellationToken);
}
