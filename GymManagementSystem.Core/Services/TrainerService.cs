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
using GymManagementSystem.Core.WebDTO.GymClass;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
using GymManagementSystem.Core.WebDTO.ScheduledClassDto;
using GymManagementSystem.Core.WebDTO.Trainer;
using GymManagementSystem.Core.WebDTO.TrainerTimeOff;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Core.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepo;
    private readonly IPersonRepository _personRepo;
    private readonly IGeneralGymRepository _generalGymRepo;
    private readonly IScheduledClassRepository _scheduledClassRepo;
    private readonly ITrainerRateRepository _trainerRateRepo;
    private readonly IPersonalBookingRepository _personalBookingRepo;
    private readonly IGymClassRepository _gymClassRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContext;
    private readonly UserManager<User> _userManager;
    public TrainerService(ITrainerRepository trainerRepo, IGeneralGymRepository generalGymRepo, ITrainerRateRepository trainerRateRepo, IUnitOfWork unitOfWork, IPersonRepository personRepo, UserManager<User> userManager, IGymClassRepository gymClassRepo, IScheduledClassRepository scheduledClassRepo, IHttpContextAccessor httpContext, IPersonalBookingRepository personalBookingRepo)
    {
        _trainerRepo = trainerRepo;
        _generalGymRepo = generalGymRepo;
        _trainerRateRepo = trainerRateRepo;
        _unitOfWork = unitOfWork;
        _personRepo = personRepo;
        _userManager = userManager;
        _gymClassRepo = gymClassRepo;
        _scheduledClassRepo = scheduledClassRepo;
        _httpContext = httpContext;
        _personalBookingRepo = personalBookingRepo;
    }

    public async Task<Result<TrainerContractCreatedResponse>> CreateTrainerContractAsync(TrainerContractAddRequest request)
    {
        TrainerContract trainer = request.ToTrainerContract();
        Person? person = await _personRepo.GetPersonByIdAsync(request.PersonId);

        if (person == null)
        {
            return Result<TrainerContractCreatedResponse>.Failure("Person not found", StatusCodeEnum.InternalServerError);
        }

        trainer.ValidFrom = DateTime.UtcNow;
        trainer.ValidTo = null;
        person.IsActive = true;
        var settings = await _generalGymRepo.GetGeneralGymDetailsAsync();

        string tempPassword = $"{Guid.NewGuid():N}".Substring(0, 12) + "!";

        User user = new User
        {
            UserName = person.Email,
            MustChangePassword = true,
            PersonId = person.Id,
            EmailConfirmed = true,
            Email = person.Email
        };

        IdentityResult createResult = await _userManager.CreateAsync(user, tempPassword);
        if (!createResult.Succeeded)
        {
            string message = string.Join("\n", createResult.Errors.Select(item => item.Description));
            return Result<TrainerContractCreatedResponse>.Failure(message, StatusCodeEnum.InternalServerError);
        }

        person.IdentityUserId = user.Id;
        if (request.TrainerType == TrainerTypeEnum.PersonalTrainer)
        {
            await _userManager.AddToRoleAsync(user, "Trainer");
        }
        else
        {
            await _userManager.AddToRoleAsync(user, "GroupInstructor");
        }

        TrainerContract trainerContract = _trainerRepo.CreateTrainerContractAsync(trainer);
        await GeneratedTrainerRates(trainerContract, settings!);
        await _unitOfWork.SaveChangesAsync();
        return Result<TrainerContractCreatedResponse>.Success(new TrainerContractCreatedResponse() { TrainerContractId = trainerContract.Id, TemporaryPassword = tempPassword }, StatusCodeEnum.Ok);
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
                RatePerSessions = generalGymDetail.DefaultRate90,
                ValidFrom = trainerContract.ValidFrom,
                ValidTo = trainerContract?.ValidTo ?? null,
            };
            TrainerRate trainerRate120 = new TrainerRate()
            {
                TrainerContractId = trainerContract!.Id,
                DurationInMinutes = 120,
                RatePerSessions = generalGymDetail.DefaultRate120,
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

    public async Task<Result<Unit>> CreateTrainerTimeOffAsync(TrainerTimeOffAddRequest entity)
    {
        Guid trainerId = entity.TrainerId ?? Guid.Empty;
        if (entity.TrainerId == null)
        {
            string? personIdClaim = _httpContext.HttpContext?.User.FindFirst("person_id")?.Value;
            if (string.IsNullOrWhiteSpace(personIdClaim) ||
               !Guid.TryParse(personIdClaim, out var personId))
            {
                return Result<Unit>.Failure(
                    "Unauthorized",
                    StatusCodeEnum.Unauthorized
                );
            }
            try
            {
                trainerId = await _personRepo.GetTrainerIdByPersonIdAsync(personId);
            }
            catch (Exception)
            {
                return Result<Unit>.Failure(
                    "Trainer not found for the user",
                    StatusCodeEnum.NotFound
                );
            }
        }

        bool isOverlap = await _trainerRepo.AnyTrainerOffOverlapAsync(trainerId, null, entity.Start, entity.End);
        if (isOverlap)
        {
            return Result<Unit>.Failure("The time range overlaps an existing time off", StatusCodeEnum.BadRequest);
        }
        TrainerTimeOff trainerTimeOff = entity.ToTrainerTimeOff();
        trainerTimeOff.TrainerId = trainerId;

        _trainerRepo.CreateTrainerTimeOffAsync(trainerTimeOff);
        await _unitOfWork.SaveChangesAsync();   
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.Ok);
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

    public async Task<Result<TrainerTimeOffReasonResponse>> GetTimeOffReasonAsync(Guid trainerTimeOffId)
    {
        string? trainerTimeOffReason = await _trainerRepo.GetTrainerTimeOffReasonAsync(trainerTimeOffId);
        TrainerTimeOffReasonResponse response = new TrainerTimeOffReasonResponse()
        {
            Reason = trainerTimeOffReason
        };
        return Result<TrainerTimeOffReasonResponse>.Success(response, StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> DeleteTrainerTimeOffAsync(Guid trainerTimeOffId)
    {
        bool isDeleted = await _trainerRepo.DeleteTrainerTimeOffAsync(trainerTimeOffId);
        if (isDeleted == false)
        {
            return Result<Unit>.Failure("Unable to delete. Not found in database", StatusCodeEnum.NotFound);
        }
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.NoContent);
    }

    public async Task<Result<IEnumerable<GymClassDto>>> GetMyGymClassesAsync()
    {
        var personIdClaim = _httpContext.HttpContext?
            .User
            .FindFirst("client_id")
            ?.Value;

        if (string.IsNullOrWhiteSpace(personIdClaim))
        {
            return Result<IEnumerable<GymClassDto>>.Failure(
                "Unauthorized",
                StatusCodeEnum.Unauthorized
            );
        }

        if (!Guid.TryParse(personIdClaim, out var personId))
        {
            return Result<IEnumerable<GymClassDto>>.Failure(
                "Invalid client_id claim",
                StatusCodeEnum.Unauthorized
            );
        }

        var classes = await _gymClassRepo
            .GetByTrainerPersonIdAsync(personId);

        return Result<IEnumerable<GymClassDto>>
            .Success(classes, StatusCodeEnum.Ok);
    }


    public async Task<Result<GroupInstructorPanelResponse>> GetGroupInstructorPanelAsync()
    {
        var personIdClaim = _httpContext.HttpContext?
            .User
            .FindFirst("person_id")
            ?.Value;

        if (string.IsNullOrWhiteSpace(personIdClaim) ||
            !Guid.TryParse(personIdClaim, out var personId))
        {
            return Result<GroupInstructorPanelResponse>.Failure(
                "Unauthorized",
                StatusCodeEnum.Unauthorized
            );
        }

        IEnumerable<ScheduledClassDto> scheduled = await _scheduledClassRepo
            .GetInstructorScheduledClasses(personId);

        GroupInstructorPanelResponse? response = await _personRepo.GetGroupInstructorPanelResponseAsync(personId);
        if (response == null)
        {
            return Result<GroupInstructorPanelResponse>.Failure("Instructor not found", StatusCodeEnum.NotFound);
        }

        response.ScheduledClasses = scheduled;

        return Result<GroupInstructorPanelResponse>
            .Success(response, StatusCodeEnum.Ok);
    }

    public async Task<Result<TrainerPanelInfoResponse>> GetPersonalTrainerPanelAsync()
    {
        var personIdClaim = _httpContext.HttpContext?
             .User
             .FindFirst("person_id")
             ?.Value;

        if (string.IsNullOrWhiteSpace(personIdClaim) ||
            !Guid.TryParse(personIdClaim, out var personId))
        {
            return Result<TrainerPanelInfoResponse>.Failure(
                "Unauthorized",
                StatusCodeEnum.Unauthorized
            );
        }

        IEnumerable<PersonalBookingForTrainerResponse> personalBookings = await _personalBookingRepo.GetPersonalBookingsAsync(personId);
        IEnumerable<TrainerTimeOffWebResponse> trainerTimeOffs = await _trainerRepo.GetTrainerTimeOffsForTrainerPanelAsync(personId);

        TrainerPanelInfoResponse? trainerPanelInfo = await _trainerRepo.GetTrainerPanelInfoResponse(personId);
        if (trainerPanelInfo == null)
        {
            return Result<TrainerPanelInfoResponse>.Failure(
                "Trainer not found",
                StatusCodeEnum.NotFound
            );
        }

        trainerPanelInfo.PersonalBookings = personalBookings;
        trainerPanelInfo.TrainerTimeOffs = trainerTimeOffs;

        return Result<TrainerPanelInfoResponse>.Success(trainerPanelInfo, StatusCodeEnum.Ok);
    }
}
