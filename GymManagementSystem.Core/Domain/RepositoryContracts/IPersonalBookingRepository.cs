using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.WebDTO.PersonalBooking;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IPersonalBookingRepository
{
    Task<IEnumerable<PersonalBooking>> GetForRangeAsync(Guid trainerId, DateOnly from, DateOnly to, CancellationToken ct);
    void AddPersonalBooking(PersonalBooking entity);
    Task<bool> CancelAsync(Guid bookingId, CancellationToken ct);
    Task<bool> DeletePersonalBookingAsync(Guid id);
    Task<PersonalBooking?> GetPersonalBookingAsync(Guid id);
    Task<PersonalBooking?> UpdatePersonalBooking(PersonalBooking personal);
    IQueryable<PersonalBooking> GetPersonalBookingsByClientId(Guid clientId);
    void DeletePersonalBooking(PersonalBooking personalBooking);
    IQueryable<PersonalBooking> GetPersonalBookingg(Guid personalBookingId);
}
