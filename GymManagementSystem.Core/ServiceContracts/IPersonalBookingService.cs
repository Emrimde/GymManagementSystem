using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
namespace GymManagementSystem.Core.ServiceContracts;

public interface IPersonalBookingService
{
    Task<Result<PersonalBookingInfoResponse>> CreatePersonalBookingAsync(PersonalBookingAddRequest entity);
    Task<Result<bool>> DeletePersonalBooking(Guid id);
    Task<Result<IEnumerable<PersonalBookingWebResponse>>> GetAllPersonalBookingsByClientIdAsync();
    Task<Result<PersonalBookingInfoResponse>> GetPersonalBookingAsync(Guid id);
    Task<Result<PersonalBookingInfoResponse>> SetStatusToPaidAsync(Guid id);
}
