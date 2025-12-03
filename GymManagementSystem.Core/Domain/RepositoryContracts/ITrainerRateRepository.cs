using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface ITrainerRateRepository
{
    Task AddRangeAsync(IEnumerable<TrainerRate> trainerRates);
}
