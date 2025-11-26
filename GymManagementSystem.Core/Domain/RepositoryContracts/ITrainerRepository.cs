using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface ITrainerRepository : IRepository<Trainer>
{
    Task<TrainerAvailabilityTemplate> CreateTrainerAvailabilityAsync(TrainerAvailabilityTemplate trainer);
}
