using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface ITrainerScheduleService
{
    Task<Result<TrainerScheduleResponse>> GetTrainerScheduleAsync(Guid trainerId, int days, CancellationToken cancellationToken);
    Task<Result<TrainerTimeOffInfoResponse>> UpdateTrainerOff(Guid id, TrainerTimeOffUpdateRequest entity);
    Task<Result<PersonalBookingInfoResponse>> CreatePersonalBookingAsync(PersonalBookingAddRequest entity);
}
