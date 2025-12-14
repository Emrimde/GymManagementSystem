using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;

using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepo;
    private readonly IGeneralGymRepository _generalGymRepo;
    private readonly ITrainerRateRepository _trainerRateRepo;
    public TrainerService(ITrainerRepository trainerRepo, IGeneralGymRepository generalGymRepo, ITrainerRateRepository trainerRateRepo)
    {
        _trainerRepo = trainerRepo;
        _generalGymRepo = generalGymRepo;
        _trainerRateRepo = trainerRateRepo;
    }

    public async Task<Result<TrainerContractInfoResponse>> CreateTrainerContractAsync(TrainerContractAddRequest entity)
    {
        TrainerContract trainer = entity.ToTrainerContract();
        var settings = await _generalGymRepo.GetGeneralGymDetailsAsync();
       
        TrainerContractInfoResponse trainerContract = await _trainerRepo.CreateTrainerContractAsync(entity.ToTrainerContract());
        await GeneratedTrainerRates(trainer, trainerContract,settings!);
        return Result<TrainerContractInfoResponse>.Success(trainerContract, StatusCodeEnum.Ok);
    }

    private async Task GeneratedTrainerRates(TrainerContract trainer, TrainerContractInfoResponse trainerContract,GeneralGymDetail generalGymDetail)
    {
        List<TrainerRate> rates = new List<TrainerRate>();

        if (trainer.TrainerType == TrainerTypeEnum.PersonalTrainer)
        {

            TrainerRate trainerRate60 = new TrainerRate()
            {
                TrainerContractId = trainerContract.Id,
                DurationInMinutes = 60,
                RatePerSessions = generalGymDetail.DefaultRate60,
                ValidFrom = trainer?.ValidFrom ?? DateTime.UtcNow,
                ValidTo = trainer?.ValidTo ?? null,
            };
            TrainerRate trainerRate90 = new TrainerRate()
            {
                TrainerContractId = trainerContract.Id,
                DurationInMinutes = 90,
                RatePerSessions = generalGymDetail.DefaultRate60,
                ValidFrom = trainer?.ValidFrom ?? DateTime.UtcNow,
                ValidTo = trainer?.ValidTo ?? null,
            };
            TrainerRate trainerRate120 = new TrainerRate()
            {
                TrainerContractId = trainerContract.Id,
                DurationInMinutes = 120,
                RatePerSessions = generalGymDetail.DefaultRate60,
                ValidFrom = trainer?.ValidFrom ?? DateTime.UtcNow,
                ValidTo = trainer?.ValidTo ?? null,
            };

            rates.Add(trainerRate60);
            rates.Add(trainerRate90);
            rates.Add(trainerRate120);
           

        }
        else if(trainer.TrainerType == TrainerTypeEnum.GroupInstructor)
        {
            TrainerRate trainerRate = new TrainerRate()
            {
                TrainerContractId = trainerContract.Id,
                DurationInMinutes = 60,
                RatePerSessions = generalGymDetail.DefaultGroupClassRate,
                ValidFrom = trainer?.ValidFrom ?? DateTime.UtcNow,
                ValidTo = trainer?.ValidTo ?? null,
            };
            rates.Add(trainerRate);
        }

        await _trainerRateRepo.AddRangeAsync(rates);
    }

    public async Task<Result<TrainerTimeOffInfoResponse>> CreateTrainerTimeOffAsync(TrainerTimeOffAddRequest entity)
    {
        bool isOverlap = await _trainerRepo.AnyTrainerOffOverlapAsync(entity.TrainerId, null ,entity.Start, entity.End);
        if (isOverlap)
        {
            return Result<TrainerTimeOffInfoResponse>.Failure("The time range overlaps an existing time off", StatusCodeEnum.BadRequest);
        }

        TrainerTimeOff addedTrainerAvailability = await _trainerRepo.CreateTrainerTimeOffAsync(entity.ToTrainerTimeOff());
        return Result<TrainerTimeOffInfoResponse>.Success(addedTrainerAvailability.ToTrainerTimeOffInfoResponse(), StatusCodeEnum.Ok);
    }

    public async Task<PageResult<TrainerContractResponse>> GetAllTrainerContractsAsync(int page , string? searchText = null , int pageSize = 50)
    {
        PageResult<TrainerContractResponse> trainerContracts = await _trainerRepo.GetAllTrainerContractsAsync(page, pageSize, searchText);

        return trainerContracts;
    }

    public async Task<Result<IEnumerable<TrainerTimeOffInfoResponse>>> GetTrainerTimeOffs(CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.TrainerTimeOff> timeOffs = await _trainerRepo.GetTrainerTimeOffs(cancellationToken);
      return Result<IEnumerable<TrainerTimeOffInfoResponse>>.Success(timeOffs.Select(item => item.ToTrainerTimeOffInfoResponse()), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<TrainerContractInfoResponse>>> GetAllGetAllInstructorsAsync(CancellationToken cancellationToken)
    {
       IEnumerable<TrainerContract> trainerContracts = await _trainerRepo.GetAllGroupInstructorsAsync(cancellationToken);
       return Result<IEnumerable<TrainerContractInfoResponse>>.Success(trainerContracts.Select(item => item.ToTrainerContractInfoResponse()), StatusCodeEnum.Ok);
    }

    public async Task<Result<TrainerContractDetailsResponse>> GetTrainerContractAsync(Guid id, bool includeDetails, CancellationToken cancellationToken)
    {
        TrainerContract? trainerContract = await _trainerRepo.GetTrainerContractAsync(id,includeDetails);
        if(trainerContract == null)
        {
            return Result<TrainerContractDetailsResponse>.Failure("Trainer not found", StatusCodeEnum.NotFound);
        }

        Console.WriteLine($"Type OK: {trainerContract.TrainerType == TrainerTypeEnum.PersonalTrainer}");
        Console.WriteLine($"ValidFrom: {trainerContract.ValidFrom:o}");
        Console.WriteLine($"UtcNow:    {DateTime.UtcNow:o}");
        Console.WriteLine($"validFromOK: {trainerContract.ValidFrom <= DateTime.UtcNow}");
       

        TrainerContractDetailsResponse response = trainerContract.ToTrainerContractDetailsResponse();
        Console.WriteLine($"Final CanShowBooking: {response.CanShowBooking}");
            return Result<TrainerContractDetailsResponse>.Success(response, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<TrainerRateResponse>>> GetAllTrainerRatesAsync(Guid id)
    {
        IEnumerable<TrainerRate> trainerRates = await _trainerRateRepo.GetTrainerRates(id);
        return Result<IEnumerable<TrainerRateResponse>>.Success(trainerRates.Select(item => item.ToTrainerRateResponse()), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<TrainerRateSelectResponse>>> GetTrainerRatesSelect(Guid id)
    {
        IEnumerable<TrainerRateSelectResponse> trainerRates = await _trainerRateRepo.GetTrainerRatesSelect(id);
        return Result<IEnumerable<TrainerRateSelectResponse>>.Success(trainerRates, StatusCodeEnum.Ok);
    }

    public async Task<Result<TrainerRateInfoResponse>> CreateTrainerRateAsync(TrainerRateAddRequest request)
    {
        IEnumerable<TrainerRate> trainerRates = await _trainerRateRepo.GetTrainerRates(request.TrainerContractId);
        TrainerRate trainerRate = new TrainerRate();
        foreach (var item in trainerRates)
        {
            if(item.DurationInMinutes == request.DurationInMinutes)
            {
                trainerRate = item;
                break;
            }
        }
        trainerRate.ValidTo = DateTime.UtcNow;
        request.ValidFrom = DateTime.UtcNow;
        TrainerRateInfoResponse response =  await _trainerRateRepo.AddTrainerRateAsync(request.ToTrainerRate());

        return Result<TrainerRateInfoResponse>.Success(response, StatusCodeEnum.Ok);
    }
}
