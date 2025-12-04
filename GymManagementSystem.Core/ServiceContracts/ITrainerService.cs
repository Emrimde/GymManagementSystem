using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface ITrainerService
{
    Task<Result<TrainerInfoResponse>> CreateAsync(TrainerAddRequest entity);
    Task<Result<TrainerContractInfoResponse>> CreateTrainerContractAsync(TrainerContractAddRequest entity);
    Task<Result<TrainerTimeOffInfoResponse>> CreateTrainerTimeOffAsync(TrainerTimeOffAddRequest entity);
    Task<Result<IEnumerable<TrainerResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<IEnumerable<TrainerContractInfoResponse>>> GetAllGetAllInstructorsAsync(CancellationToken cancellationToken);
    Task<Result<IEnumerable<TrainerContractResponse>>> GetAllTrainerContractsAsync(CancellationToken cancellationToken);
    Task<Result<TrainerDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<TrainerContractDetailsResponse>> GetTrainerContractAsync(Guid id, bool includeDetails, CancellationToken cancellationToken);
    Task<Result<IEnumerable<TrainerTimeOffInfoResponse>>> GetTrainerTimeOffs(CancellationToken cancellationToken);
    Task<Result<TrainerInfoResponse>> UpdateAsync(Guid id, TrainerUpdateRequest entity);
}