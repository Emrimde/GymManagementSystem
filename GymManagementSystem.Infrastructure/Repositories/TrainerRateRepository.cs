using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;

namespace GymManagementSystem.Infrastructure.Repositories;

public class TrainerRateRepository : ITrainerRateRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TrainerRateRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<TrainerRate> trainerRates)
    {
      await _dbContext.TrainerRates.AddRangeAsync(trainerRates);
      await _dbContext.SaveChangesAsync();
    }
}
