using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface ITrainerTimeOffRepository
{
    Task<IEnumerable<TrainerTimeOff>> GetForRangeAsync(Guid trainerId, DateOnly from, DateOnly to, CancellationToken ct);
    Task<TrainerTimeOff> AddAsync(TrainerTimeOff entity, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}
