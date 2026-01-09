using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.WebDTO.PersonalBooking;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IPersonalBookingRepository
{
    Task<IEnumerable<PersonalBooking>> GetForRangeAsync(Guid trainerId, DateOnly from, DateOnly to, CancellationToken ct);
    Task<PersonalBooking> AddAsync(PersonalBooking entity);
    Task<bool> CancelAsync(Guid bookingId, CancellationToken ct);
    Task<bool> DeletePersonalBookingAsync(Guid id);
    Task<PersonalBooking?> GetPersonalBookingAsync(Guid id);
    Task<PersonalBooking?> UpdatePersonalBooking(PersonalBooking personal);
    Task<IEnumerable<PersonalBookingWebResponse>> GetAllPersonalBookingsByClientIdAsync(Guid clientId);
}
