using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.TrainerContract;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface ITrainerRepository
{
    Task<bool> AnyTrainerOffOverlapAsync(Guid trainerId,Guid? trainerOffId, DateTime start, DateTime end);
    Task<TrainerTimeOff> CreateTrainerTimeOffAsync(TrainerTimeOff trainerTimeOff);
    Task<IEnumerable<TrainerTimeOff>> GetTrainerTimeOffs(CancellationToken cancellationToken);
    Task<bool> AnyPersonalBookingOverlapAsync(Guid trainerId, DateTime start, DateTime end);
    Task<TrainerContractInfoResponse> CreateTrainerContractAsync(TrainerContract trainerContract);
    Task<IEnumerable<TrainerContract>> GetAllTrainerContractsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TrainerContract>> GetAllGroupInstructorsAsync(CancellationToken cancellationToken);
    Task<TrainerContract?> GetTrainerContractAsync(Guid id,bool includeDetails);
}
