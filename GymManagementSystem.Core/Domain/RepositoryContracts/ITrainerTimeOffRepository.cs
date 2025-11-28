using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.TrainerTimeOff;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface ITrainerTimeOffRepository
{
    Task<IEnumerable<Entities.TrainerTimeOff>> GetForRangeAsync(Guid trainerId, DateOnly from, DateOnly to, CancellationToken ct);
    Task<Entities.TrainerTimeOff> AddAsync(Entities.TrainerTimeOff entity, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
    Task<TrainerTimeOff> UpdateTrainerOffAsync(Guid id, TrainerTimeOff entity);
}
