using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface ITrainerService
{
    Task<Result<TrainerInfoResponse>> CreateAsync(TrainerAddRequest entity, CancellationToken cancellationToken);
    Task<Result<IEnumerable<TrainerResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<TrainerDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<TrainerInfoResponse>> UpdateAsync(Guid id, TrainerUpdateRequest entity, CancellationToken cancellationToken);
}