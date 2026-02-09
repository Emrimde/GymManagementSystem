using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class PersonalBookingRepository : IPersonalBookingRepository
{
    private readonly ApplicationDbContext _dbContext;
    public PersonalBookingRepository(ApplicationDbContext db)
    {
        _dbContext = db;
    }

    public void AddPersonalBooking(PersonalBooking entity)
    {
        _dbContext.PersonalBookings.Add(entity);
    }

    public async Task<bool> CancelAsync(Guid bookingId, CancellationToken ct)
    {
        var booking = await _dbContext.PersonalBookings
            .FirstOrDefaultAsync(b => b.Id == bookingId, ct);

        if (booking == null)
            return false;

        booking.Status = BookingStatus.Cancelled;
        await _dbContext.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeletePersonalBookingAsync(Guid id)
    {
        PersonalBooking? personal = await _dbContext.PersonalBookings.FirstOrDefaultAsync(item => item.Id == id);
        if (personal == null)
        {
            return false;
        }
        _dbContext.PersonalBookings.Remove(personal);
        int deleted = _dbContext.SaveChanges();
        return deleted > 0;
    }

    public IQueryable<PersonalBooking> GetPersonalBookingsByClientId(Guid clientId)
    {
        return _dbContext.PersonalBookings
            .Where(item => item.ClientId == clientId);
    }

    public async Task<IEnumerable<PersonalBooking>> GetForRangeAsync(
    Guid trainerId, DateOnly from, DateOnly to, CancellationToken ct)
    {
        var start = DateTime.SpecifyKind(
            from.ToDateTime(TimeOnly.MinValue),
            DateTimeKind.Utc);

        var end = DateTime.SpecifyKind(
            to.AddDays(1).ToDateTime(TimeOnly.MinValue),
            DateTimeKind.Utc);

        return await _dbContext.PersonalBookings
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
       return await _dbContext.PersonalBookings.FirstOrDefaultAsync(item => item.Id == id); 
    }
    public IQueryable<PersonalBooking> GetPersonalBookingg(Guid personalBookingId)
    {
        return _dbContext.PersonalBookings.Where(item => item.Id == personalBookingId);
    }

    public async Task<PersonalBooking?> UpdatePersonalBooking(PersonalBooking booking)
    {
        _dbContext.PersonalBookings.Update(booking);
        await _dbContext.SaveChangesAsync();
        return booking;
    }

    public void DeletePersonalBooking(PersonalBooking personalBooking)
    {
        _dbContext.PersonalBookings.Remove(personalBooking);   
    }

    public async Task<IEnumerable<PersonalBookingForTrainerResponse>> GetPersonalBookingsAsync(Guid personId)
    {
        return await _dbContext.PersonalBookings.Where(item => item.Start >= DateTime.UtcNow && item.TrainerContract.PersonId == personId).Select(item => new PersonalBookingForTrainerResponse()
        {
            ClientName = item.Client.FirstName + " " + item.Client.LastName,
            Date = item.Start.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
            Duration = item.TrainerRate.DurationInMinutes.ToString() + " minutes",
            PersonalBookingId = item.Id,
            EndTime = (item.Start.ToLocalTime() + TimeSpan.FromMinutes(item.TrainerRate.DurationInMinutes)).ToString(),
            Price = item.Price.ToString() + "$"
        }).ToListAsync();
    }
}
