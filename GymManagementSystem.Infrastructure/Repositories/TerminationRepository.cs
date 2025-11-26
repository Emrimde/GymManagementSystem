using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class TerminationRepository : IRepository<Termination>
{
    private readonly ApplicationDbContext _dbContext;

    public TerminationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Termination?> CreateAsync(Termination entity)
    {
       _dbContext.Terminations.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<Termination>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Terminations.Include(item => item.Client).ToListAsync(cancellationToken);
    }

    public Task<Termination?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Termination?> UpdateAsync(Guid id, Termination entity)
    {
        throw new NotImplementedException();
    }
}
