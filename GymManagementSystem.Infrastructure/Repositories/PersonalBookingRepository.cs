using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
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

    public async Task<PersonalBooking> AddAsync(PersonalBooking entity, CancellationToken ct)
    {
        await _db.PersonalBookings.AddAsync(entity, ct);
        await _db.SaveChangesAsync(ct);
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
                item.TrainerId == trainerId &&
                item.Status == BookingStatus.Booked &&
                item.Start >= start &&
                item.Start < end)
            .ToListAsync(ct);
    }

}
