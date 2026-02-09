using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.WebDTO.Trainer;
using GymManagementSystem.Core.WebDTO.TrainerTimeOff;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface ITrainerRepository
{
    Task<bool> AnyTrainerOffOverlapAsync(Guid trainerId,Guid? trainerOffId, DateTime start, DateTime end);
    Task<TrainerTimeOff> CreateTrainerTimeOffAsync(TrainerTimeOff trainerTimeOff);
    Task<IEnumerable<TrainerTimeOff>> GetTrainerTimeOffs(CancellationToken cancellationToken);
    Task<bool> AnyPersonalBookingOverlapAsync(Guid trainerId,  Guid? personalBookingId, DateTime start, DateTime end);
    TrainerContract CreateTrainerContractAsync(TrainerContract trainerContract);
    Task<PageResult<TrainerContractResponse>> GetAllTrainerContractsAsync(int page = 1, int pageSize = 50 ,string? searchText = null);
    Task<IEnumerable<TrainerContractInfoResponse>> GetAllGroupInstructorsAsync(CancellationToken cancellationToken);
    Task<TrainerContract?> GetTrainerContractAsync(Guid id,bool includeDetails);
    Task<IEnumerable<TrainerInfoResponse>> GetAllPersonalTrainersAsync();
    void DeleteTrainer(TrainerContract trainerContract);
    Task<string?> GetTrainerTimeOffReasonAsync(Guid trainerTimeOffId);
    Task<bool> DeleteTrainerTimeOffAsync(Guid trainerTimeOffId);
    Task<TrainerPanelInfoResponse?> GetTrainerPanelInfoResponse(Guid personId);
    Task<IEnumerable<TrainerTimeOffWebResponse>> GetTrainerTimeOffsForTrainerPanelAsync(Guid personId);
}
