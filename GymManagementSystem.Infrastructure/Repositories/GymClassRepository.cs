using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class GymClassRepository : IGymClassRepository
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

    public Task<PageResult<GymClassResponse>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null)
    {
        throw new NotImplementedException();
    }

    public Task<GymClass?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<GymClass?> UpdateAsync(Guid id, GymClass entity)
    {
        throw new NotImplementedException();
    }
}
