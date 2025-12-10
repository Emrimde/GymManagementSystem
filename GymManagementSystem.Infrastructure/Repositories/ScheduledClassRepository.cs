using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
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

    public async Task AddRangeAsync(IEnumerable<ScheduledClass> entities)
    {
        await _dbContext.ScheduledClasses.AddRangeAsync(entities);
        await _dbContext.SaveChangesAsync();
    }

    public Task<ScheduledClass> CreateAsync(ScheduledClass entity)
    {
        throw new NotImplementedException();
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
                query = query.Where(item => item.GymClass!.Name.ToLower().Contains(term));
                                                
            }
        }



        int totalCount = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        List<ScheduledClassResponse> list = await query.OrderBy(item => item.GymClass!.Name)
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

    public async Task<ScheduledClass?> GetByIdAsync(Guid id)
    {
        return await _dbContext.ScheduledClasses.Include(item => item.ClassBookings).FirstOrDefaultAsync(item => item.Id == id);
    }

    public Task<ScheduledClass?> UpdateAsync(Guid id, ScheduledClass entity)
    {
        throw new NotImplementedException();
    }
}
