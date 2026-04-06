using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.QueryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.QueryServices;
public class GymClassQueryService : IGymClassQueryService
{
    private readonly ApplicationDbContext _dbContext;
    public GymClassQueryService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<GymClassResponse>> GetGymClassesResponseAsync(bool? isActive)
    {
        var data = await _dbContext.GymClasses
            .Where(item => !isActive.HasValue || item.IsActive == isActive.Value)
            .Select(item => new
            {
                item.Id,
                item.Name,
                item.StartHour,
                item.Duration,
                item.MaxPeople,
                item.DaysOfWeek,
                item.CreatedAt,
                item.UpdatedAt,
                item.IsActive
            })
            .ToListAsync();

        return data.Select(item => new GymClassResponse
        {
            Id = item.Id,
            Name = item.Name,
            StartHour = item.StartHour.ToString(@"hh\:mm"),
            Duration = item.Duration.ToString(@"hh\:mm"),
            EndTime = (item.StartHour + item.Duration).ToString(@"hh\:mm"),
            MaxPeople = item.MaxPeople,
            Days = item.DaysOfWeek.ToString(),
            CreatedAt = item.CreatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
            UpdatedAt = item.UpdatedAt.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
            IsActive = item.IsActive,
            CanActivate = !item.IsActive
        }).ToList();
    }
}
