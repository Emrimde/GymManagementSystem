using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerAvailabilityTemplate;
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


    public async Task<Result<TrainerAvailabilityInfoResponse>> CreateTrainerAvailabilityAsync(TrainerAvailabilityAddRequest entity)
    {
        TrainerAvailabilityTemplate addedTrainerAvailability = await _trainerRepo.CreateTrainerAvailabilityAsync(entity.ToTrainerAvailabilityTemplate());
        return Result<TrainerAvailabilityInfoResponse>.Success(addedTrainerAvailability.ToTrainerAvailabilityInfoResponse(), StatusCodeEnum.Ok);
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

    public Task<Result<TrainerInfoResponse>> UpdateAsync(Guid id, TrainerUpdateRequest entity)
    {
        throw new NotImplementedException();
    }
}
