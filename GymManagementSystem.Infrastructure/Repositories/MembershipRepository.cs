using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class MembershipRepository : IMembershipRepository
{
    private readonly ApplicationDbContext _dbContext;
    public MembershipRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Membership> CreateAsync(Membership entity)
    {
        _dbContext.Memberships.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<PageResult<MembershipResponse>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null)
    {
        IQueryable<Membership> query = _dbContext.Memberships;

        if (searchText != null)
        {
            string searchTextlower = searchText.ToLower();
            string[] terms = searchTextlower.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string term in terms)
            {
                string pattern = $"%{term}%";
                query = query.Where(item => item.Name.ToLower().Contains(term));
                                                
            }
        }



        int totalCount = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        List<MembershipResponse> list = await query.OrderBy(item => item.Name)
                                                    .Skip((page - 1) * pageSize)
                                                        .Take(pageSize)
                                                            .Select(item => item.ToMembershipResponse())
                                                                .ToListAsync();



        return new PageResult<MembershipResponse>()
        {
            Items = list,
            TotalCount = totalCount,
            PageSize = pageSize,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }

    public async Task<IEnumerable<MembershipResponse>> GetAllMemberships()
    {
        IQueryable<Membership> query = _dbContext.Memberships;
        return await query.Select(item => item.ToMembershipResponse()).ToListAsync();
    }

    public async Task<Membership?> GetByIdAsync(Guid id)
    {
       return await _dbContext.Memberships.FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<Membership?> UpdateAsync(Guid id, Membership entity)
    {
        Membership? membership =  await _dbContext.Memberships.FirstOrDefaultAsync(item => item.Id == id);

        if (membership == null)
        {
            return null!;
        }

        membership.Price = entity.Price;
        membership.Name = entity.Name;
        await _dbContext.SaveChangesAsync();
        return membership;
    }
}
