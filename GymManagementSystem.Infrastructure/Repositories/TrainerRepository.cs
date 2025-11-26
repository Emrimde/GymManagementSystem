using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TrainerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Trainer> CreateAsync(Trainer entity)
    {
       _dbContext.Add(entity);
       await _dbContext.SaveChangesAsync();
       return entity;
    }

    public async Task<TrainerAvailabilityTemplate> CreateTrainerAvailabilityAsync(TrainerAvailabilityTemplate trainerAvailability)
    {
        _dbContext.TrainerAvailabilityTemplates.Add(trainerAvailability);
        await _dbContext.SaveChangesAsync();
        return trainerAvailability;
    }

    public async Task<IEnumerable<Trainer>> GetAllAsync(CancellationToken cancellationToken)
    {
       return await _dbContext.Trainers.ToListAsync(cancellationToken); 
    }

    public async Task<Trainer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Trainers.FirstOrDefaultAsync(item => item.Id == id);
    }

    public Task<Trainer?> UpdateAsync(Guid id, Trainer entity)
    {
        throw new NotImplementedException();
    }
}
