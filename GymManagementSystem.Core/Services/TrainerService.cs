using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Core.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepo;
    private readonly IPersonRepository _personRepo;
    private readonly IGeneralGymRepository _generalGymRepo;
    private readonly ITrainerRateRepository _trainerRateRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    public TrainerService(ITrainerRepository trainerRepo, IGeneralGymRepository generalGymRepo, ITrainerRateRepository trainerRateRepo, IUnitOfWork unitOfWork, IPersonRepository personRepo, UserManager<User> userManager)
    {
        _trainerRepo = trainerRepo;
        _generalGymRepo = generalGymRepo;
        _trainerRateRepo = trainerRateRepo;
        _unitOfWork = unitOfWork;
        _personRepo = personRepo;
        _userManager = userManager;
    }

    public async Task<Result<TrainerContractInfoResponse>> CreateTrainerContractAsync(TrainerContractAddRequest request)
    {
        TrainerContract trainer = request.ToTrainerContract();
        Person? person = await _personRepo.GetPersonByIdAsync(request.PersonId);

        if (person == null)
        {
            return Result<TrainerContractInfoResponse>.Failure("Person not found", StatusCodeEnum.InternalServerError);
        }

        trainer.ValidFrom = DateTime.UtcNow;
        trainer.ValidTo = null;
        person.IsActive = true;
        var settings = await _generalGymRepo.GetGeneralGymDetailsAsync();

        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        var random = new string(
            Enumerable.Range(0, 3)
                .Select(_ => chars[Random.Shared.Next(chars.Length)])
                .ToArray()
        );

        User user = new User
        {
            UserName = $"{person.FirstName}{person.LastName}{random}"
                .Replace(" ", "")
                .ToLower()
        };

        var createResult = await _userManager.CreateAsync(user, "trainer");
        if (!createResult.Succeeded)
        {
            return Result<TrainerContractInfoResponse>.Failure($"Creating new trainer failed", StatusCodeEnum.InternalServerError);
        }

        person.IdentityUserId = user.Id;

        await _userManager.AddToRoleAsync(user, "Trainer");

        TrainerContract trainerContract = _trainerRepo.CreateTrainerContractAsync(trainer);
        await GeneratedTrainerRates(trainerContract, settings!);
        await _unitOfWork.SaveChangesAsync();
        return Result<TrainerContractInfoResponse>.Success(new TrainerContractInfoResponse() { Id = trainerContract.Id }, StatusCodeEnum.Ok);
    }

    private async Task GeneratedTrainerRates(TrainerContract trainerContract, GeneralGymDetail generalGymDetail)
    {
        List<TrainerRate> rates = new List<TrainerRate>();

        if (trainerContract.TrainerType == TrainerTypeEnum.PersonalTrainer)
        {

            TrainerRate trainerRate60 = new TrainerRate()
            {
                TrainerContractId = trainerContract.Id,
                DurationInMinutes = 60,
                RatePerSessions = generalGymDetail.DefaultRate60,
                ValidFrom = trainerContract.ValidFrom,
                ValidTo = trainerContract?.ValidTo ?? null,
            };
            TrainerRate trainerRate90 = new TrainerRate()
            {
                TrainerContractId = trainerContract!.Id,
                DurationInMinutes = 90,
                RatePerSessions = generalGymDetail.DefaultRate60,
                ValidFrom = trainerContract.ValidFrom,
                ValidTo = trainerContract?.ValidTo ?? null,
            };
            TrainerRate trainerRate120 = new TrainerRate()
            {
                TrainerContractId = trainerContract!.Id,
                DurationInMinutes = 120,
                RatePerSessions = generalGymDetail.DefaultRate60,
                ValidFrom = trainerContract.ValidFrom,
                ValidTo = trainerContract?.ValidTo ?? null,
            };

            rates.Add(trainerRate60);
            rates.Add(trainerRate90);
            rates.Add(trainerRate120);


        }
        else if (trainerContract.TrainerType == TrainerTypeEnum.GroupInstructor)
        {
            TrainerRate trainerRate = new TrainerRate()
            {
                TrainerContractId = trainerContract.Id,
                DurationInMinutes = 60,
                RatePerSessions = generalGymDetail.DefaultGroupClassRate,
                ValidFrom = trainerContract.ValidFrom,
                ValidTo = trainerContract?.ValidTo ?? null,
            };
            rates.Add(trainerRate);
        }

        await _trainerRateRepo.AddRangeAsync(rates);
    }

    public async Task<Result<TrainerTimeOffInfoResponse>> CreateTrainerTimeOffAsync(TrainerTimeOffAddRequest entity)
    {
        bool isOverlap = await _trainerRepo.AnyTrainerOffOverlapAsync(entity.TrainerId, null, entity.Start, entity.End);
        if (isOverlap)
        {
            return Result<TrainerTimeOffInfoResponse>.Failure("The time range overlaps an existing time off", StatusCodeEnum.BadRequest);
        }

        TrainerTimeOff addedTrainerAvailability = await _trainerRepo.CreateTrainerTimeOffAsync(entity.ToTrainerTimeOff());
        return Result<TrainerTimeOffInfoResponse>.Success(addedTrainerAvailability.ToTrainerTimeOffInfoResponse(), StatusCodeEnum.Ok);
    }

    public async Task<PageResult<TrainerContractResponse>> GetAllTrainerContractsAsync(int page, string? searchText = null, int pageSize = 50)
    {
        PageResult<TrainerContractResponse> trainerContracts = await _trainerRepo.GetAllTrainerContractsAsync(page, pageSize, searchText);

        return trainerContracts;
    }

    public async Task<Result<IEnumerable<TrainerTimeOffInfoResponse>>> GetTrainerTimeOffs(CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Entities.TrainerTimeOff> timeOffs = await _trainerRepo.GetTrainerTimeOffs(cancellationToken);
        return Result<IEnumerable<TrainerTimeOffInfoResponse>>.Success(timeOffs.Select(item => item.ToTrainerTimeOffInfoResponse()), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<TrainerContractInfoResponse>>> GetAllInstructorsAsync(CancellationToken cancellationToken)
    {
        IEnumerable<TrainerContractInfoResponse> trainerContracts = await _trainerRepo.GetAllGroupInstructorsAsync(cancellationToken);
        return Result<IEnumerable<TrainerContractInfoResponse>>.Success(trainerContracts, StatusCodeEnum.Ok);
    }

    public async Task<Result<TrainerContractDetailsResponse>> GetTrainerContractAsync(Guid id, bool includeDetails, CancellationToken cancellationToken)
    {
        TrainerContract? trainerContract = await _trainerRepo.GetTrainerContractAsync(id, includeDetails);
        if (trainerContract == null)
        {
            return Result<TrainerContractDetailsResponse>.Failure("Trainer not found", StatusCodeEnum.NotFound);
        }

        TrainerContractDetailsResponse response = trainerContract.ToTrainerContractDetailsResponse();
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
            if (item.DurationInMinutes == request.DurationInMinutes)
            {
                trainerRate = item;
                break;
            }
        }
        trainerRate.ValidTo = DateTime.UtcNow;
        request.ValidFrom = DateTime.UtcNow;
        TrainerRateInfoResponse response = await _trainerRateRepo.AddTrainerRateAsync(request.ToTrainerRate());

        return Result<TrainerRateInfoResponse>.Success(response, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<TrainerInfoResponse>>> GetAllPersonalTrainersAsync()
    {
        IEnumerable<TrainerInfoResponse> dto = await _trainerRepo.GetAllPersonalTrainersAsync();
        return Result<IEnumerable<TrainerInfoResponse>>.Success(dto, StatusCodeEnum.Ok);
    }
}
