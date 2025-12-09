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

    public async Task AddRangeAsync(IEnumerable<ScheduledClass> entities)
    {
        await _dbContext.ScheduledClasses.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public Task<ScheduledClass> CreateAsync(ScheduledClass entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ScheduledClass>> GetAllAsync()
    {
        return await _dbContext.ScheduledClasses.Include(item => item.GymClass).ToListAsync();
    }

    public async Task<ScheduledClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.ScheduledClasses.Include(item => item.ClassBookings).FirstOrDefaultAsync(item => item.Id == id ,cancellationToken);
    }

    public Task<ScheduledClass?> UpdateAsync(Guid id, ScheduledClass entity)
    {
        throw new NotImplementedException();
    }
}
