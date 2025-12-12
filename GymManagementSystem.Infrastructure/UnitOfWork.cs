using GymManagementSystem.Core.Domain;
using GymManagementSystem.Infrastructure.DatabaseContext;

namespace GymManagementSystem.Infrastructure;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)  =>  await _dbContext.SaveChangesAsync(cancellationToken);
}
