using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.Result;
namespace GymManagementSystem.Core.ServiceContracts;

public interface IPersonalBookingService
{
    Task<Result<PersonalBookingInfoResponse>> CreatePersonalBookingAsync(PersonalBookingAddRequest entity);
    Task<Result<bool>> DeletePersonalBooking(Guid id);
    Task<Result<PersonalBookingInfoResponse>> GetPersonalBookingAsync(Guid id);
    Task<Result<PersonalBookingInfoResponse>> SetStatusToPaidAsync(Guid id);
}
