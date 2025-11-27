using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface ITrainerScheduleService
{
    Task<Result<TrainerScheduleResponse>> GetTrainerScheduleAsync(Guid trainerId, int days, CancellationToken cancellationToken);
}
