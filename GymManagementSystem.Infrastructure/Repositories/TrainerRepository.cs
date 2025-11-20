using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class TrainerRepository : IRepository<Trainer>
{
    private readonly ApplicationDbContext _dbContext;
    public TrainerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Trainer> CreateAsync(Trainer entity, CancellationToken cancellationToken)
    {
       _dbContext.Add(entity);
       await _dbContext.SaveChangesAsync(cancellationToken);
       return entity;
    }

    public async Task<IEnumerable<Trainer>> GetAllAsync(CancellationToken cancellationToken)
    {
       return await _dbContext.Trainers.ToListAsync(cancellationToken); 
    }

    public Task<Trainer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Trainer?> UpdateAsync(Guid id, Trainer entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
