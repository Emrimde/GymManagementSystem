using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.WebDTO.ScheduledClassDto;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class ScheduledClassRepository : IScheduledClassRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ScheduledClassRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddRange(IEnumerable<ScheduledClass> entities)
    {
        _dbContext.ScheduledClasses.AddRangeAsync(entities);
    }

    public void Create(ScheduledClass entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteScheduledClassByGymClassIdAsync(Guid gymClassId)
    {
        await _dbContext.ScheduledClasses.Where(item => item.GymClassId == gymClassId).ExecuteDeleteAsync();
    }

    public async Task<PageResult<ScheduledClassResponse>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null)
    {
        IQueryable<ScheduledClass> query = _dbContext.ScheduledClasses;

        if (searchText != null)
        {
            string searchTextlower = searchText.ToLower();
            string[] terms = searchTextlower.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string term in terms)
            {
                string pattern = $"%{term}%";
                query = query.Where(item => item.GymClass!.Name.ToLower().Contains(term) );
                                                
            }
        }



        int totalCount = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        List<ScheduledClassResponse> list = await query.Include(item => item.GymClass).OrderBy(item => item.GymClass!.Name)
                                                    .Skip((page - 1) * pageSize)
                                                        .Take(pageSize)
                                                            .Select(item => item.ToScheduledClassResponse())
                                                                .ToListAsync();



        return new PageResult<ScheduledClassResponse>()
        {
            Items = list,
            TotalCount = totalCount,
            PageSize = pageSize,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }

    public async Task<IEnumerable<ScheduledClassResponse>> GetAllScheduledClasses(Guid gymClassId)
    {
        IQueryable<ScheduledClass> query = _dbContext.ScheduledClasses.Where(item => item.GymClassId == gymClassId && item.Date >= DateTime.UtcNow && item.IsActive);

        return await query
            .Include(item => item.GymClass).OrderBy(item => item.Date)
            .Select(item => item.ToScheduledClassResponse())
            .ToListAsync();
    }

    public async Task<IEnumerable<ScheduledClass>> GetAllScheduledClassesByGymClassId(Guid gymClassId, int? classBookingDaysInAdvanceCount, bool showActive)
    {
        IQueryable<ScheduledClass> scheduledClasses = _dbContext.ScheduledClasses.Where(item => item.GymClass!.Id == gymClassId && item.Date >= DateTime.UtcNow).Include(item => item.GymClass);

        if(classBookingDaysInAdvanceCount != null)
        {
          scheduledClasses = scheduledClasses.Where(item => item.Date <= DateTime.UtcNow.AddDays(classBookingDaysInAdvanceCount.Value));
        }
        if(showActive)
        {
          scheduledClasses = scheduledClasses.Where(item => item.IsActive).Include(item => item.ClassBookings);
        }

        return await scheduledClasses.ToListAsync();
    }

    public async Task<IEnumerable<ScheduledClass>> GetAllScheduledClasses()
    {
        IQueryable<ScheduledClass> scheduledClasses = _dbContext.ScheduledClasses.Where(item => item.GymClass.IsActive && item.Date >= DateTime.UtcNow).Include(item => item.GymClass);

        return await scheduledClasses.ToListAsync();
    }

    public async Task<ScheduledClass?> GetByIdAsync(Guid id)
    {
        return await _dbContext.ScheduledClasses.Include(item => item.ClassBookings).ThenInclude(item => item.Client).FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<IEnumerable<ScheduledClass>> GetFutureUnbookedByGymClassId(Guid gymClassId)
    {
        return await _dbContext.ScheduledClasses.Where(item => !item.ClassBookings.Any() && item.Date >= DateTime.UtcNow && item.GymClassId == gymClassId).ToListAsync();
    }

    public Task<ScheduledClass?> UpdateAsync(Guid id, ScheduledClass entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ScheduledClassDto>> GetInstructorScheduledClasses(Guid personId)
    {
        return await _dbContext.ScheduledClasses
            .Where(item => item.GymClass.Trainer != null && item.GymClass.Trainer.Person.Id == personId)
            .OrderBy(item => item.Date)
            .Select(item => new ScheduledClassDto
            {
                Id = item.Id,
                Name = item.GymClass.Name,
                Date = item.Date.ToLocalTime().ToString("dd.MM.yyyy"),
                StartFrom = item.StartFrom.ToString(@"hh\:mm"),
                StartTo = (item.StartFrom + TimeSpan.FromHours(1)).ToString(@"hh\:mm"),
                MaxPeople = item.MaxPeople,
                IsCancelled = item.IsCancelled,
                IsActive = item.IsActive
            })
            .ToListAsync();
    }
}
