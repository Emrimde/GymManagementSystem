using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class ScheduledClassRepository : IScheduledClassRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ScheduledClassRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<ScheduledClass> entities, CancellationToken cancellationToken)
    {
        await _dbContext.ScheduledClasses.AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<ScheduledClass> CreateAsync(ScheduledClass entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ScheduledClass>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.ScheduledClasses.ToListAsync(cancellationToken);
    }

    public Task<ScheduledClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ScheduledClass?> UpdateAsync(Guid id, ScheduledClass entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
