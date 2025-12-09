using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class GymClassRepository : IRepository<GymClass>
{
    private readonly ApplicationDbContext _dbContext;
    public GymClassRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GymClass> CreateAsync(GymClass entity)
    {
        _dbContext.GymClasses.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<GymClass>> GetAllAsync()
    {
        return await _dbContext.GymClasses.ToListAsync();  
    }

    public Task<GymClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<GymClass?> UpdateAsync(Guid id, GymClass entity)
    {
        throw new NotImplementedException();
    }
}
