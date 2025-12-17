using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO;
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

    public async Task<IEnumerable<VisitResponse>> GetAllClientVisits(Guid clientId)
    {
        return await _dbContext.Visits.Where(item => item.ClientId == clientId)
            .OrderByDescending(item => item.VisitDate).Take(25)
            .Select(item => new VisitResponse
            {
                VisitSource = item.VisitSource,
                VisitDate = item.VisitDate.ToString("dd:MM:yyyy - HH:mm"),
            })
            .ToListAsync();
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
