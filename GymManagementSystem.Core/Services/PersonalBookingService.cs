using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Core.Services;

public class PersonalBookingService : IPersonalBookingService
{
    private readonly IPersonalBookingRepository _personalBookingRepo;
    private readonly ITrainerRepository _trainerRepo;
    private readonly ITrainerRateRepository _trainerRateRepo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;
    public PersonalBookingService(IPersonalBookingRepository personalBookingRepo, ITrainerRepository trainerRepo, ITrainerRateRepository trainerRateRepo, IHttpContextAccessor contextAccessor, IUnitOfWork unitOfWork)
    {
        _personalBookingRepo = personalBookingRepo;
        _trainerRepo = trainerRepo;
        _trainerRateRepo = trainerRateRepo;
        _contextAccessor = contextAccessor;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<PersonalBookingInfoResponse>> CreatePersonalBookingAsync(PersonalBookingAddRequest entity)
    {
        TrainerRateResponse? trainerRate = await _trainerRateRepo.GetTrainerRateByIdAsync(entity.TrainerRateId);
        if (trainerRate == null)
        {
            return Result<PersonalBookingInfoResponse>.Failure("Cannot find trainer rate", StatusCodeEnum.NotFound);
        }

        DateTime localStart = entity.StartDay.Date + entity.StartHour;

        DateTime start = DateTime.SpecifyKind(localStart, DateTimeKind.Local)
                                  .ToUniversalTime();

        DateTime end = start.AddMinutes(trainerRate.DurationInMinutes);

        bool isOverlap = await _trainerRepo.AnyPersonalBookingOverlapAsync(entity.TrainerId,null, start, end);
        if (isOverlap)
        {
            return Result<PersonalBookingInfoResponse>.Failure("The time range overlaps an existing personal booking", StatusCodeEnum.BadRequest);
        }

        TrainerContract? trainerContract = await _trainerRepo.GetTrainerContractAsync(entity.TrainerId, false);
        if (trainerContract == null)
        {
            return Result<PersonalBookingInfoResponse>.Failure("Personal trainer not found", StatusCodeEnum.NotFound);
        }
        if (trainerContract.ValidTo.HasValue)
        {
            if (trainerContract.ValidTo.Value.Date <= DateTime.UtcNow.Date || entity.StartDay.Date >= trainerContract.ValidTo.Value.Date)
            {
                return Result<PersonalBookingInfoResponse>.Failure("Trainer contract is not valid, so you can't add personal training for this trainer", StatusCodeEnum.BadRequest);
            }
        }

        if (start < DateTime.UtcNow + TimeSpan.FromHours(5))
        {
            return Result<PersonalBookingInfoResponse>.Failure("Personal training must be registered at least 5 hours before", StatusCodeEnum.BadRequest);
        }

        PersonalBooking personalBooking = entity.ToPersonalBooking();
        personalBooking.Start = start;
        personalBooking.End = end;
        personalBooking.Price = trainerRate.RatePerSessions;
        personalBooking.TrainerRateId = trainerRate.Id;

        if (entity.IsClientReservation)
        {
            string? claim = _contextAccessor.HttpContext?.User.FindFirst("client_id")?.Value;
            if (!Guid.TryParse(claim, out var clientId))
            {
                return Result<PersonalBookingInfoResponse>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
            }
            personalBooking.ClientId = clientId;
        }

        else
        {
            personalBooking.ClientId = entity.ClientId;
        }

        _personalBookingRepo.AddPersonalBooking(personalBooking);
        await _unitOfWork.SaveChangesAsync();
        return Result<PersonalBookingInfoResponse>.Success(personalBooking.ToPersonalBookingInfoResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<bool>> DeletePersonalBooking(Guid id)
    {
        bool isDeleted = await _personalBookingRepo.DeletePersonalBookingAsync(id);
        if (!isDeleted)
        {
            return Result<bool>.Failure("Personal booking not deleted", StatusCodeEnum.InternalServerError);
        }
        return Result<bool>.Success(isDeleted, StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> DeletePersonalBookingAsync(Guid personalBookingId)
    {
        PersonalBooking? personalBooking = await _personalBookingRepo.GetPersonalBookingAsync(personalBookingId);
        if (personalBooking == null)
        {
            return Result<Unit>.Failure("Personal booking not found", StatusCodeEnum.NotFound);
        }
        _personalBookingRepo.DeletePersonalBooking(personalBooking);
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<PersonalBookingResponse>>> GetAllClientPersonalBookings(Guid clientId)
    {
        IEnumerable<PersonalBookingResponse> response = await _personalBookingRepo.GetPersonalBookingsByClientId(clientId).Select(item => new PersonalBookingResponse
        {
            PersonalBookingId = item.Id,
            TrainerFullName = item.TrainerContract != null ? item.TrainerContract.Person.FirstName + " " + item.TrainerContract.Person.LastName : "",
            Date = item.Start.ToLocalTime().ToString("dd.MM.yyyy"),
            StartEndTime = $"{item.Start.ToLocalTime().ToString("HH:mm")} - {item.End.ToLocalTime().ToString("HH:mm")}",
            BookingStatus = item.Status.ToString(),
            Price = item.Price.ToString(),
        }).ToListAsync();

        return Result<IEnumerable<PersonalBookingResponse>>.Success(response, StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<PersonalBookingWebResponse>>> GetAllPersonalBookingsByClientIdAsync()
    {
        string? claim = _contextAccessor.HttpContext?.User.FindFirst("client_id")?.Value;
        if (!Guid.TryParse(claim, out var clientId))
        {
            return Result<IEnumerable<PersonalBookingWebResponse>>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
        }

        IEnumerable<PersonalBookingWebResponse> personalBookings = await _personalBookingRepo.GetPersonalBookingsByClientId(clientId).Where(item => item.ClientId == clientId)
            .Select(item => new PersonalBookingWebResponse
            {
                TrainerFullName = item.TrainerContract != null ? item.TrainerContract.Person.FirstName + " " + item.TrainerContract.Person.LastName : "",
                Date = item.Start.ToLocalTime().ToString("dd.MM.yyyy"),
                StartEndTime = $"{item.Start.ToString("HH:mm")} - {item.End.ToString("HH:mm")}"
            })
            .ToListAsync(); ;

        return Result<IEnumerable<PersonalBookingWebResponse>>.Success(personalBookings, StatusCodeEnum.Unauthorized);
    }

    public async Task<Result<PersonalBookingInfoResponse>> GetPersonalBookingAsync(Guid id)
    {
        PersonalBooking? personal = await _personalBookingRepo.GetPersonalBookingAsync(id);
        if (personal == null)
        {
            return Result<PersonalBookingInfoResponse>.Failure("Personal booking not found", StatusCodeEnum.NotFound);
        }
        return Result<PersonalBookingInfoResponse>.Success(personal.ToPersonalBookingInfoResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<PersonalBookingForEditResponse>> GetPersonalBookingForEditAsync(Guid personalBookingId)
    {
        PersonalBookingForEditResponse? personalBooking = await _personalBookingRepo.GetPersonalBookingg(personalBookingId).Select(item => new PersonalBookingForEditResponse()
        {
            ClientId = item.ClientId,
            StartDate = item.Start.ToLocalTime(),
            TrainerId = item.TrainerContractId,
            TrainerRateId = item.TrainerRateId.HasValue ? item.TrainerRateId.Value : Guid.Empty,
        }).FirstOrDefaultAsync();

        if(personalBooking == null)
        {
            return Result<PersonalBookingForEditResponse>.Failure("Personal booking not found", StatusCodeEnum.NotFound);
        }
        return Result<PersonalBookingForEditResponse>.Success(personalBooking, StatusCodeEnum.Ok);
    }

    public async Task<Result<PersonalBookingInfoResponse>> SetStatusToPaidAsync(Guid id)
    {
        PersonalBooking? personal = await _personalBookingRepo.GetPersonalBookingAsync(id);
        if (personal == null)
        {
            return Result<PersonalBookingInfoResponse>.Failure("Personal booking not found", StatusCodeEnum.NotFound);
        }
        personal.Status = BookingStatus.PaidByClient;
        PersonalBooking? updatePersonal = await _personalBookingRepo.UpdatePersonalBooking(personal);
        return Result<PersonalBookingInfoResponse>.Success(updatePersonal!.ToPersonalBookingInfoResponse(), StatusCodeEnum.NotFound);
    }

    public async Task<Result<Unit>> UpdatePersonalBookingAsync(Guid personalBookingId, PersonalBookingUpdateRequest personalBookingUpdateRequest)
    {
        PersonalBooking? personalBooking = await _personalBookingRepo.GetPersonalBookingg(personalBookingId).FirstOrDefaultAsync();
        if(personalBooking == null)
        {
            return Result<Unit>.Failure("Personal booking not found", StatusCodeEnum.NotFound);
        }

        TrainerRateResponse? trainerRate = await _trainerRateRepo.GetTrainerRateByIdAsync(personalBookingUpdateRequest.TrainerRateId);
        if (trainerRate == null)
        {
            return Result<Unit>.Failure("Cannot find trainer rate", StatusCodeEnum.NotFound);
        }


        DateTime start = DateTime.SpecifyKind(personalBookingUpdateRequest.StartDay + personalBookingUpdateRequest.StartHour,DateTimeKind.Local).ToUniversalTime();
        DateTime end = start.AddMinutes(trainerRate.DurationInMinutes);
        bool isOverlap = await _trainerRepo.AnyPersonalBookingOverlapAsync(personalBookingUpdateRequest.TrainerId, personalBookingId, start, end);
        if (isOverlap)
        {
            return Result<Unit>.Failure("The time range overlaps an existing personal booking", StatusCodeEnum.BadRequest);
        }

        TrainerContract? trainerContract = await _trainerRepo.GetTrainerContractAsync(personalBookingUpdateRequest.TrainerId, false);
        if (trainerContract == null)
        {
            return Result<Unit>.Failure("Personal trainer not found", StatusCodeEnum.NotFound);
        }
        if (trainerContract.ValidTo.HasValue)
        {
            if (trainerContract.ValidTo.Value.Date <= DateTime.UtcNow.Date || personalBookingUpdateRequest.StartDay.Date >= trainerContract.ValidTo.Value.Date)
            {
                return Result<Unit>.Failure("Trainer contract is not valid, so you can't add personal training for this trainer", StatusCodeEnum.BadRequest);
            }
        }

        if (start < DateTime.UtcNow + TimeSpan.FromHours(5))
        {
            return Result<Unit>.Failure("Personal training must be registered at least 5 hours before", StatusCodeEnum.BadRequest);
        }

        personalBooking.Start = start;
        personalBooking.TrainerContractId = personalBookingUpdateRequest.TrainerId;
        personalBooking.TrainerRateId = personalBookingUpdateRequest.TrainerRateId;
        personalBooking.End = end;

        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.NoContent);
    }
}
