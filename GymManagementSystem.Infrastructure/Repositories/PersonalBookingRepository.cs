using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class PersonalBookingRepository : IPersonalBookingRepository
{
    private readonly ApplicationDbContext _db;
    public PersonalBookingRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<PersonalBooking> AddAsync(PersonalBooking entity)
    {
        _db.PersonalBookings.Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> CancelAsync(Guid bookingId, CancellationToken ct)
    {
        var booking = await _db.PersonalBookings
            .FirstOrDefaultAsync(b => b.Id == bookingId, ct);

        if (booking == null)
            return false;

        booking.Status = BookingStatus.Cancelled;
        await _db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeletePersonalBookingAsync(Guid id)
    {
        PersonalBooking? personal = await _db.PersonalBookings.FirstOrDefaultAsync(item => item.Id == id);
        _db.PersonalBookings.Remove(personal);
        if (personal == null)
        {
            return false;
        }
        int deleted = _db.SaveChanges();
        return deleted > 0;
    }

    public async Task<IEnumerable<PersonalBookingWebResponse>> GetAllPersonalBookingsByClientIdAsync(Guid clientId)
    {
        return await _db.PersonalBookings.Where(item => item.ClientId == clientId)
            .Select(item => new PersonalBookingWebResponse
            {
                TrainerFullName = item.TrainerContract.Person.FirstName + " " + item.TrainerContract.Person.LastName,
                Date = item.Start.ToString("dd:MM:yyyy"),
                StartEndTime = $"{item.Start.ToString("HH:mm")} - {item.End.ToString("HH:mm")}"
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<PersonalBooking>> GetForRangeAsync(
    Guid trainerId, DateOnly from, DateOnly to, CancellationToken ct)
    {
        // Początek zakresu (UTC)
        var start = DateTime.SpecifyKind(
            from.ToDateTime(TimeOnly.MinValue),
            DateTimeKind.Utc);

        // Koniec zakresu = początek dnia PO 'to' (UTC, exclusive)
        var end = DateTime.SpecifyKind(
            to.AddDays(1).ToDateTime(TimeOnly.MinValue),
            DateTimeKind.Utc);

        return await _db.PersonalBookings
            .AsNoTracking()
            .Include(p => p.Client)
            .Where(item =>
                item.TrainerContractId == trainerId &&
                (item.Status == BookingStatus.Booked || item.Status == BookingStatus.PaidByClient) &&
                item.Start >= start &&
                item.Start < end)
            .ToListAsync(ct);
    }

    public async Task<PersonalBooking?> GetPersonalBookingAsync(Guid id)
    {
       return await _db.PersonalBookings.FirstOrDefaultAsync(item => item.Id == id); 
    }

    public async Task<PersonalBooking?> UpdatePersonalBooking(PersonalBooking booking)
    {
        _db.PersonalBookings.Update(booking);
        await _db.SaveChangesAsync();
        return booking;
    }

}
