using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class ClassBookingRepository : IRepository<ClassBookingResponse,ClassBooking>
{
    private readonly ApplicationDbContext _dbContext;
    public ClassBookingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ClassBooking> CreateAsync(ClassBooking entity)
    {
        _dbContext.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<PageResult<ClassBookingResponse>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null)
    {
        IQueryable<ClassBooking> query = _dbContext.ClassBookings;
        if(searchText != null)
        {
            string searchTextLower = searchText.ToLower();
            string[] terms = searchTextLower.Split(' ',StringSplitOptions.RemoveEmptyEntries);
            foreach(string term in terms)
            {
                string pattern = $"%{term}%";
                query = query.Where(item => item.Client!.FirstName.ToLower().Contains(term) ||
                                            item.Client.LastName.ToLower().Contains(term) ||
                                                item.Client.PhoneNumber.Contains(term));
            }
        }

        int totalCount = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);


        List<ClassBookingResponse> list = await query.OrderBy(item => item.Client!.FirstName)
                                                            .Skip((page - 1) * pageSize)
                                                                .Take(pageSize)
                                                                    .Select(item => item.ToClassBookingResponse())
                                                                        .ToListAsync();

        return new PageResult<ClassBookingResponse>
        {
            CurrentPage = page,
            Items = list,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };

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
