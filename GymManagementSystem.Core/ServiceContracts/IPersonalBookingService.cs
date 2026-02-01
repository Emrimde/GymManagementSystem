using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
using Microsoft.AspNetCore.Mvc;
namespace GymManagementSystem.Core.ServiceContracts;

public interface IPersonalBookingService
{
    Task<Result<PersonalBookingInfoResponse>> CreatePersonalBookingAsync(PersonalBookingAddRequest entity);
    Task<Result<bool>> DeletePersonalBooking(Guid id);
    Task<Result<Unit>> DeletePersonalBookingAsync(Guid clientId);
    Task<Result<IEnumerable<PersonalBookingResponse>>> GetAllClientPersonalBookings(Guid clientId);
    Task<Result<IEnumerable<PersonalBookingWebResponse>>> GetAllPersonalBookingsByClientIdAsync();
    Task<Result<PersonalBookingInfoResponse>> GetPersonalBookingAsync(Guid id);
    Task<Result<PersonalBookingForEditResponse>> GetPersonalBookingForEditAsync(Guid personalBookingId);
    Task<Result<PersonalBookingInfoResponse>> SetStatusToPaidAsync(Guid id);

}
