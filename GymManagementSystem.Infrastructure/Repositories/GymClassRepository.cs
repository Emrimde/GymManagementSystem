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
    public void CreateAsync(GymClass entity)
    {
        _dbContext.GymClasses.Add(entity);
    }

    public async Task<IEnumerable<GymClass>> GetAllAsync()
    {
        return await _dbContext.GymClasses.ToListAsync();  
    }

    public async Task<GymClass?> GetByIdAsync(Guid id)
    {
        return await _dbContext.GymClasses.FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<IEnumerable<GymClassComboBoxResponse>> GetGymClassesForSelectAsync()
    {
       return await _dbContext.GymClasses.Select(item => new GymClassComboBoxResponse() { 
            GymClassId = item.Id,
            Name = item.Name,
        }).ToListAsync();
    }

    public Task<GymClass?> UpdateAsync(Guid id, GymClass entity)
    {
        throw new NotImplementedException();
    }
}
