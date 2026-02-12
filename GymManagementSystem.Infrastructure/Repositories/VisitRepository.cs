using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.DTO.Dashboard;
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
                Id = item.Id,
                VisitSource = item.VisitSource,
                VisitDate = item.VisitDate.ToLocalTime().ToString("dd:MM:yyyy - HH:mm"),
                GuestName = item.GuestName ?? string.Empty
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

    public async Task<IEnumerable<PointResponse>> GetAllVisitsOverTime(DateTime startTime, DateTime endTime)
    {
        
        List<PointResponse> points = await _dbContext.Visits.Where(item => startTime <= item.VisitDate.Date && endTime >= item.VisitDate.Date).GroupBy(item => item.VisitDate.Date).Select(item => new PointResponse()
        {
            Date = item.Key,
            TimeSeriesPoint = item.Count()
        }).ToListAsync();

        return points;
    }

    public async Task<int> GetTotalVisitsAsync(DateTime? date)
    {
        IQueryable<Visit> query = _dbContext.Visits;
        if (date != null)
        {
            DateTime startTime = date.Value.Date;
            DateTime endTime = startTime.AddDays(1);

            query = query.Where(item => item.VisitDate >= startTime && item.VisitDate < endTime);
        }

        return await query.CountAsync();
    }

    public async Task<Visit?> GetVisitById(Guid visitId)
    {
       return await _dbContext.Visits.Where(item => item.Id == visitId).FirstOrDefaultAsync();
    }
    public void DeleteVisit(Visit visit)
    {
        _dbContext.Visits.Remove(visit);
    }

    public async Task<int> GetFriendVisitsCountForClientInMonthAsync(Guid clientId)
    {
        DateTime now = DateTime.UtcNow;
        DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);
        startOfMonth = DateTime.SpecifyKind(startOfMonth, DateTimeKind.Utc);
        DateTime endOfMonth = startOfMonth.AddMonths(1);
        return await _dbContext.Visits.CountAsync(item => item.ClientId == clientId
            && item.IsWithGuest == true
            && item.VisitDate >= startOfMonth
            && item.VisitDate < endOfMonth);
    }
}
