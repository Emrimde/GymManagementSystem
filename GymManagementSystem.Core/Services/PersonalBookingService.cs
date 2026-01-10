using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
using Microsoft.AspNetCore.Http;
using static System.Net.WebRequestMethods;

namespace GymManagementSystem.Core.Services;

public class PersonalBookingService : IPersonalBookingService
{
    private readonly IPersonalBookingRepository _personalBookingRepo;
    private readonly ITrainerRepository _trainerRepo;
    private readonly ITrainerRateRepository _trainerRateRepo;
    private readonly IHttpContextAccessor _contextAccessor;
    public PersonalBookingService(IPersonalBookingRepository personalBookingRepo, ITrainerRepository trainerRepo, ITrainerRateRepository trainerRateRepo, IHttpContextAccessor contextAccessor)
    {
        _personalBookingRepo = personalBookingRepo;
        _trainerRepo = trainerRepo;
        _trainerRateRepo = trainerRateRepo;
        _contextAccessor = contextAccessor;
    }
    public async Task<Result<PersonalBookingInfoResponse>> CreatePersonalBookingAsync(PersonalBookingAddRequest entity)
    {
        TrainerRateResponse? trainerRate = await _trainerRateRepo.GetTrainerRateByIdAsync(entity.TrainerRateId);
        if(trainerRate == null)
        {
            return Result<PersonalBookingInfoResponse>.Failure("Cannot find trainer rate", StatusCodeEnum.NotFound);
        }
        if(entity.StartDay == DateTime.MinValue)
        {
            return Result<PersonalBookingInfoResponse>.Failure("No date selected", StatusCodeEnum.BadRequest);
        }
        DateTime start = entity.StartDay.Date + entity.StartHour;
        DateTime end = start + TimeSpan.FromMinutes(trainerRate.DurationInMinutes);
        start = DateTime.SpecifyKind(start, DateTimeKind.Utc);
        end = DateTime.SpecifyKind(end, DateTimeKind.Utc);
        bool isOverlap = await _trainerRepo.AnyPersonalBookingOverlapAsync(entity.TrainerId, start, end);
        if (isOverlap)
        {
            return Result<PersonalBookingInfoResponse>.Failure("The time range overlaps an existing personal booking", StatusCodeEnum.BadRequest);
        }
        
        TrainerContract? trainerContract = await _trainerRepo.GetTrainerContractAsync(entity.TrainerId, false);
        if(trainerContract?.ValidFrom >= DateTime.UtcNow || trainerContract?.IsSigned == false)
        {
            return Result<PersonalBookingInfoResponse>.Failure("Trainer contract is not valid, so you can't add personal training for this trainer", StatusCodeEnum.BadRequest);
        }

        if(start <  DateTime.UtcNow + TimeSpan.FromHours(5))
        {
            return Result<PersonalBookingInfoResponse>.Failure("Personal training must be registered at least 5 hours before", StatusCodeEnum.BadRequest);
        }

        PersonalBooking personalBooking = entity.ToPersonalBooking();
        personalBooking.Start = start;
        personalBooking.End = end;
        personalBooking.Price = trainerRate.RatePerSessions;
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

        PersonalBooking createdPersonalBooking = await _personalBookingRepo.AddAsync(personalBooking);
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

    public async Task<Result<IEnumerable<PersonalBookingWebResponse>>> GetAllPersonalBookingsByClientIdAsync()
    {
        string? claim = _contextAccessor.HttpContext?.User.FindFirst("client_id")?.Value;
        if (!Guid.TryParse(claim, out var clientId))
        {
            return Result<IEnumerable<PersonalBookingWebResponse>>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
        }

        IEnumerable<PersonalBookingWebResponse> personalBookings = await _personalBookingRepo.GetAllPersonalBookingsByClientIdAsync(clientId);

        return Result<IEnumerable<PersonalBookingWebResponse>>.Success(personalBookings, StatusCodeEnum.Unauthorized);
    }

    public async Task<Result<PersonalBookingInfoResponse>> GetPersonalBookingAsync(Guid id)
    {
        PersonalBooking? personal = await _personalBookingRepo.GetPersonalBookingAsync(id);
        if(personal == null)
        {
            return Result<PersonalBookingInfoResponse>.Failure("Personal booking not found", StatusCodeEnum.NotFound);
        }
        return Result<PersonalBookingInfoResponse>.Success(personal.ToPersonalBookingInfoResponse(), StatusCodeEnum.Ok);
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
}
