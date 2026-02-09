using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.GymClass;
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

    public async Task<IEnumerable<GymClass>> GetAllAsync(bool? isActive)
    {
        IQueryable<GymClass> query = _dbContext.GymClasses;

        if (isActive.HasValue)
        {
            query = query.Where(item => item.IsActive == isActive);
        }
        return await query.ToListAsync();  
    }

    public async Task<GymClass?> GetByIdAsync(Guid id)
    {
        return await _dbContext.GymClasses.FirstOrDefaultAsync(item => item.Id == id);
    }
    public async Task<GymClass?> GetGymClassWithScheduledClassesAsync(Guid id)
    {
        return await _dbContext.GymClasses.Include(item => item.ScheduledClasses).ThenInclude(item => item.ClassBookings).FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<IEnumerable<GymClassComboBoxResponse>> GetGymClassesForSelectAsync()
    {
       return await _dbContext.GymClasses.Where(item => item.IsActive).Select(item => new GymClassComboBoxResponse() { 
            GymClassId = item.Id,
            Name = item.Name
        }).ToListAsync();
    }

    public Task<GymClass?> UpdateAsync(Guid id, GymClass entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<GymClassDto>> GetByTrainerPersonIdAsync(Guid personId)
    {
        return await _dbContext.GymClasses
            .Where(item => item.Trainer != null && item.Trainer.PersonId == personId)
            .OrderBy(item => item.Name)
            .Select(item => new GymClassDto
            {
                Id = item.Id,
                Name = item.Name,
                StartHour = item.StartHour,
                Duration = item.Duration,
                DaysOfWeek = item.DaysOfWeek,
                MaxPeople = item.MaxPeople
            })
            .ToListAsync();
    }

    
}
