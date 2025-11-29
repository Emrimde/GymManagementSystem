using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerAvailabilityTemplate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepo;
    public TrainerService(ITrainerRepository trainerRepo)
    {
        _trainerRepo = trainerRepo;
    }

    public async Task<Result<TrainerInfoResponse>> CreateAsync(TrainerAddRequest entity)
    {
        Trainer addedTrainer = await _trainerRepo.CreateAsync(entity.ToTrainer());
        return Result<TrainerInfoResponse>.Success(addedTrainer.ToTrainerInfoResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<TrainerTimeOffInfoResponse>> CreateTrainerTimeOffAsync(TrainerTimeOffAddRequest entity)
    {
        bool isOverlap = await _trainerRepo.AnyTrainerOffOverlapAsync(entity.TrainerId,entity.Start, entity.End);
        if (isOverlap)
        {
            return Result<TrainerTimeOffInfoResponse>.Failure("The time range overlaps an existing time off", StatusCodeEnum.BadRequest);
        }

        TrainerTimeOff addedTrainerAvailability = await _trainerRepo.CreateTrainerTimeOffAsync(entity.ToTrainerTimeOff());
        return Result<TrainerTimeOffInfoResponse>.Success(addedTrainerAvailability.ToTrainerTimeOffInfoResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<TrainerResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Trainer> trainers = await _trainerRepo.GetAllAsync(cancellationToken);
        return Result<IEnumerable<TrainerResponse>>.Success(trainers.Select(item => item.ToTrainerResponse()), StatusCodeEnum.Ok);
    }

    public async Task<Result<TrainerDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Trainer? trainer = await _trainerRepo.GetByIdAsync(id, cancellationToken);
        if (trainer == null)
        {
            return Result<TrainerDetailsResponse>.Failure("Trainer not found", StatusCodeEnum.NotFound);
        }
        return Result<TrainerDetailsResponse>.Success(trainer.ToTrainerDetailsResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<TrainerTimeOffInfoResponse>>> GetTrainerTimeOffs(CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.TrainerTimeOff> timeOffs = await _trainerRepo.GetTrainerTimeOffs(cancellationToken);
      return Result<IEnumerable<TrainerTimeOffInfoResponse>>.Success(timeOffs.Select(item => item.ToTrainerTimeOffInfoResponse()), StatusCodeEnum.Ok);
    }

    public Task<Result<TrainerInfoResponse>> UpdateAsync(Guid id, TrainerUpdateRequest entity)
    {
        throw new NotImplementedException();
    }
}
