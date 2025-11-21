using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class ClassBookingRepository : IRepository<ClassBooking>
{
    private readonly ApplicationDbContext _dbContext;
    public ClassBookingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ClassBooking> CreateAsync(ClassBooking entity, CancellationToken cancellationToken)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<IEnumerable<ClassBooking>> GetAllAsync(CancellationToken cancellationToken)
    {
       return await _dbContext.ClassBookings.ToListAsync(cancellationToken);
    }

    public async Task<ClassBooking?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.ClassBookings.FirstOrDefaultAsync(item => item.Id == id,cancellationToken);
    }

    public Task<ClassBooking?> UpdateAsync(Guid id, ClassBooking entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
