using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface ITrainerRepository
{
    Task<bool> AnyTrainerOffOverlapAsync(Guid trainerId,Guid? trainerOffId, DateTime start, DateTime end);
    Task<TrainerTimeOff> CreateTrainerTimeOffAsync(TrainerTimeOff trainerTimeOff);
    Task<IEnumerable<TrainerTimeOff>> GetTrainerTimeOffs(CancellationToken cancellationToken);
    Task<bool> AnyPersonalBookingOverlapAsync(Guid trainerId, DateTime start, DateTime end);
    TrainerContract CreateTrainerContractAsync(TrainerContract trainerContract);
    Task<PageResult<TrainerContractResponse>> GetAllTrainerContractsAsync(int page = 1, int pageSize = 50 ,string? searchText = null);
    Task<IEnumerable<TrainerContract>> GetAllGroupInstructorsAsync(CancellationToken cancellationToken);
    Task<TrainerContract?> GetTrainerContractAsync(Guid id,bool includeDetails);
}
