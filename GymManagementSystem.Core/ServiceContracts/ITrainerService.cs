using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.WebDTO.GymClass;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
using GymManagementSystem.Core.WebDTO.ScheduledClassDto;
using GymManagementSystem.Core.WebDTO.Trainer;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.Core.ServiceContracts;

public interface ITrainerService
{
    Task<Result<TrainerContractCreatedResponse>> CreateTrainerContractAsync(TrainerContractAddRequest entity);
    Task<Result<TrainerRateInfoResponse>> CreateTrainerRateAsync(TrainerRateAddRequest request);
    Task<Result<Unit>> CreateTrainerTimeOffAsync(TrainerTimeOffAddRequest entity);
    Task<Result<Unit>> DeleteTrainerTimeOffAsync(Guid trainerTimeOffId);

    Task<Result<IEnumerable<TrainerContractInfoResponse>>> GetAllInstructorsAsync(CancellationToken cancellationToken);
    Task<Result<IEnumerable<TrainerInfoResponse>>> GetAllPersonalTrainersAsync();
    Task<Result<IEnumerable<TrainerRateResponse>>> GetAllTrainerRatesAsync(Guid id, bool? showActive);
    Task<Result<TrainerTimeOffReasonResponse>> GetTimeOffReasonAsync(Guid trainerTimeOffId);
    Task<Result<TrainerContractDetailsResponse>> GetTrainerContractAsync(Guid id, bool includeDetails, CancellationToken cancellationToken);
    Task<Result<IEnumerable<TrainerRateSelectResponse>>> GetTrainerRatesSelect(Guid id);
    Task<Result<IEnumerable<TrainerTimeOffInfoResponse>>> GetTrainerTimeOffs(CancellationToken cancellationToken);
    Task<Result<IEnumerable<GymClassDto>>> GetMyGymClassesAsync();
    Task<Result<GroupInstructorPanelResponse>> GetGroupInstructorPanelAsync();
    Task<Result<TrainerPanelInfoResponse>> GetPersonalTrainerPanelAsync();
}