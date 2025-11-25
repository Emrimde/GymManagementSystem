using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class TrainerService : ITrainerService
{
    private readonly IRepository<Trainer> _trainerRepo;
    public TrainerService(IRepository<Trainer> trainerRepo)
    {
        _trainerRepo = trainerRepo;
    }

    public async Task<Result<TrainerInfoResponse>> CreateAsync(TrainerAddRequest entity, CancellationToken cancellationToken)
    {
        Trainer addedTrainer = await _trainerRepo.CreateAsync(entity.ToTrainer(), cancellationToken);
        return Result<TrainerInfoResponse>.Success(addedTrainer.ToTrainerInfoResponse(), StatusCodeEnum.Ok);
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

    public Task<Result<TrainerInfoResponse>> UpdateAsync(Guid id, TrainerUpdateRequest entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
