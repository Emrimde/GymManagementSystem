using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;

namespace GymManagementSystem.Core.ServiceContracts;

public interface ITrainerService
{
    //Task<Result<TrainerInfoResponse>> CreateAsync(TrainerAddRequest entity);
    Task<Result<TrainerContractInfoResponse>> CreateTrainerContractAsync(TrainerContractAddRequest entity);
    Task<Result<TrainerRateInfoResponse>> CreateTrainerRateAsync(TrainerRateAddRequest request);
    Task<Result<Unit>> CreateTrainerTimeOffAsync(TrainerTimeOffAddRequest entity);
    //Task<Result<IEnumerable<TrainerResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<IEnumerable<TrainerContractInfoResponse>>> GetAllInstructorsAsync(CancellationToken cancellationToken);
    Task<Result<IEnumerable<TrainerInfoResponse>>> GetAllPersonalTrainersAsync();
    Task<PageResult<TrainerContractResponse>> GetAllTrainerContractsAsync(int page, string? searchText = null, int pageSize = 50);
    Task<Result<IEnumerable<TrainerRateResponse>>> GetAllTrainerRatesAsync(Guid id);

    //Task<Result<TrainerDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<TrainerContractDetailsResponse>> GetTrainerContractAsync(Guid id, bool includeDetails, CancellationToken cancellationToken);
    Task<Result<IEnumerable<TrainerRateSelectResponse>>> GetTrainerRatesSelect(Guid id);
    Task<Result<IEnumerable<TrainerTimeOffInfoResponse>>> GetTrainerTimeOffs(CancellationToken cancellationToken);
    //Task<Result<TrainerInfoResponse>> UpdateAsync(Guid id, TrainerUpdateRequest entity);
}