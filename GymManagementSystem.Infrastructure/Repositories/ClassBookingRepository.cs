using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.ClassBooking.ReadModel;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
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

    public async Task<int> CountClassBookingsByScheduledClassId(Guid scheduleClassId)
    {
        return await _dbContext.ClassBookings.CountAsync(item => item.ScheduledClassId == scheduleClassId);
    }

    public async Task<ClassBooking> CreateAsync(ClassBooking entity)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<ClassBookingReadModel>> GetAllClassBookingsByClientId(Guid clientId)
    {
        return await _dbContext.ClassBookings.Where(item => item.ClientId == clientId && item.ScheduledClass.Date >= DateTime.UtcNow && item.IsActive).Select(item => new ClassBookingReadModel()
        {
            Id = item.Id,
            CreatedAt = item.CreatedAt,
            Date = item.ScheduledClass!.Date,
            StartFrom = item.ScheduledClass.StartFrom,
            StartTo = item.ScheduledClass.StartTo,
            Name = item.ScheduledClass!.GymClass!.Name,
        }).ToListAsync();
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
