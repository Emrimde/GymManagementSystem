using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly ApplicationDbContext _dbContext;
    public VisitRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddVisit(Visit visit)
    {
        _dbContext.Visits.Add(visit);
    }

    public async Task<DateTime> GetLastVisitDateByClientId(Guid clientId)
    {
       return await _dbContext.Visits
            .Where(item => item.ClientId == clientId)
            .OrderByDescending(item => item.VisitDate)
            .Select(item => item.VisitDate)
            .FirstOrDefaultAsync();
    }

    public async Task<int> GetTotalVisitsByClientId(Guid clientId)
    {
        return await _dbContext.Visits.CountAsync(item => item.ClientId == clientId);
    }
}
