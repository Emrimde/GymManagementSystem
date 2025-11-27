using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface ITrainerRepository : IRepository<Trainer>
{
    Task<bool> AnyOverlapAsync(Guid trainerId, DateTime start, DateTime end);
    Task<TrainerTimeOff> CreateTrainerTimeOffAsync(TrainerTimeOff trainerTimeOff);
    Task<IEnumerable<TrainerTimeOff>> GetTrainerTimeOffs(CancellationToken cancellationToken);
}
