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

    public async Task<TrainerTimeOff> CreateTrainerTimeOffAsync(TrainerTimeOff trainerTimeOff)
    {
        _dbContext.TrainerTimeOff.Add(trainerTimeOff);
        await _dbContext.SaveChangesAsync();
        return trainerTimeOff;
    }

    public Task<bool> AnyTrainerOffOverlapAsync(Guid trainerId, DateTime start, DateTime end)
    {
        return _dbContext.TrainerTimeOff
            .AnyAsync(item =>
                item.TrainerId == trainerId &&
                item.Start < end &&
                item.End > start
            );
    }

    public Task<bool> AnyPersonalBookingOverlapAsync(Guid trainerId, DateTime start, DateTime end)
    {
        return _dbContext.PersonalBookings
            .AnyAsync(item =>
                item.TrainerId == trainerId &&
                item.Start < end &&
                item.End > start
            );
    }

    public async Task<IEnumerable<Trainer>> GetAllAsync(CancellationToken cancellationToken)
    {
       return await _dbContext.Trainers.ToListAsync(cancellationToken); 
    }

    public async Task<Trainer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Trainers.Include(item => item.AvailabilityTemplate).FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<IEnumerable<TrainerTimeOff>> GetTrainerTimeOffs(CancellationToken cancellationToken)
    {
        IEnumerable<TrainerTimeOff> list = await _dbContext.TrainerTimeOff.ToListAsync();
        return list;
    }

    public Task<Trainer?> UpdateAsync(Guid id, Trainer entity)
    {
        throw new NotImplementedException();
    }
}
