using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class ClassBookingRepository : IClassBookingRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ClassBookingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(ClassBooking entity)
    {
        _dbContext.Add(entity);
    }

    public async Task DeleteClassBookingsByGymClassIdAsync(Guid gymClassId)
    {
       await _dbContext.ClassBookings.Where(item => item.ScheduledClass.GymClassId == gymClassId).ExecuteDeleteAsync();
    }

    public async Task<bool> DeleteClassBookingAsync(Guid classBookingId)
    {
        int affected = await _dbContext.ClassBookings.Where(item => item.Id == classBookingId).ExecuteDeleteAsync();
        return affected > 0;
    }

    public async Task<IEnumerable<ClassBookingResponse>> GetAllClassBookingsByClientId(Guid clientId)
    {
        return await _dbContext.ClassBookings.Where(item => item.ClientId == clientId && item.ScheduledClass.Date >= DateTime.UtcNow && item.IsActive).Select(item => new ClassBookingResponse
        {
            Id = item.Id,
            Name = item.ScheduledClass!.GymClass!.Name,
            StartFrom = item.ScheduledClass.StartFrom.ToString(@"hh\:mm"),
            StartTo = (item.ScheduledClass.StartFrom + TimeSpan.FromMinutes(60)).ToString(@"hh\:mm"),
            Date = item.ScheduledClass.Date.ToString("dd.MM.yyyy"),
            CreatedAt = item.CreatedAt.ToLocalTime().ToString("dd.MM.yyyy"),
        })
        .ToListAsync();
    }

    public async Task<ClassBooking?> GetByIdAsync(Guid id)
    {
        return await _dbContext.ClassBookings.FirstOrDefaultAsync(item => item.Id == id);
    }

    public Task<ClassBooking?> UpdateAsync(Guid id, ClassBooking entity)
    {
        throw new NotImplementedException();
    }
}
