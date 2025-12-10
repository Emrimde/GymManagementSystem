using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class TerminationRepository : IRepository<TerminationResponse,Termination>
{
    private readonly ApplicationDbContext _dbContext;

    public TerminationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Termination?> CreateAsync(Termination entity)
    {
       _dbContext.Terminations.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<PageResult<TerminationResponse>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null)
    {
        IQueryable<Termination> query = _dbContext.Terminations;

        if (searchText != null)
        {
            string searchTextlower = searchText.ToLower();
            string[] terms = searchTextlower.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string term in terms)
            {
                string pattern = $"%{term}%";
                query = query.Where(item => item.Client!.FirstName.ToLower().Contains(term) ||
                                                item.Client.LastName.ToLower().Contains(term) ||
                                                    item.Client.PhoneNumber.Contains(term) ||
                                                        item.Client.Email.ToLower().Contains(term));
            }
        }



        int totalCount = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        List<TerminationResponse> list = await query.OrderBy(item => item.Client!.FirstName)
                                                    .Skip((page - 1) * pageSize)
                                                        .Take(pageSize)
                                                            .Select(item => item.ToTerminationResponse())
                                                                .ToListAsync();



        return new PageResult<TerminationResponse>()
        {
            Items = list,
            TotalCount = totalCount,
            PageSize = pageSize,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }

    public Task<Termination?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Termination?> UpdateAsync(Guid id, Termination entity)
    {
        throw new NotImplementedException();
    }
}
