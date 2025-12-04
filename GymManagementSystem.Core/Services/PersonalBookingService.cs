using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class PersonalBookingService : IPersonalBookingService
{
    private readonly IPersonalBookingRepository _personalBookingRepo;
    private readonly ITrainerRepository _trainerRepo;
    public PersonalBookingService(IPersonalBookingRepository personalBookingRepo, ITrainerRepository trainerRepo)
    {
        _personalBookingRepo = personalBookingRepo;
        _trainerRepo = trainerRepo;
    }
    public async Task<Result<PersonalBookingInfoResponse>> CreatePersonalBookingAsync(PersonalBookingAddRequest entity)
    {
        bool isOverlap = await _trainerRepo.AnyPersonalBookingOverlapAsync(entity.TrainerId, entity.Start, entity.End);
        if (isOverlap)
        {
            return Result<PersonalBookingInfoResponse>.Failure("The time range overlaps an existing personal booking", StatusCodeEnum.BadRequest);
        }
        PersonalBooking personalBooking = await _personalBookingRepo.AddAsync(entity.ToPersonalBooking());
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
