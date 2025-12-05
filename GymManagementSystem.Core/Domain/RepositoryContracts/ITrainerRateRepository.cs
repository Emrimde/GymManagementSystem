using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.TrainerRate;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface ITrainerRateRepository
{
    Task AddRangeAsync(IEnumerable<TrainerRate> trainerRates);
    Task<IEnumerable<TrainerRate>> GetTrainerRates(Guid id);
    Task<TrainerRateResponse?> GetTrainerRateByIdAsync(Guid id);
    Task<IEnumerable<TrainerRateSelectResponse>> GetTrainerRatesSelect(Guid id);
}
