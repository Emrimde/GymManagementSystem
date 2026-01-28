using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.WebDTO.ClientMembership;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class ClientMembershipRepository : IClientMembershipRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ClientMembershipRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateAsync(ClientMembership entity)
    {
        _dbContext.ClientMemberships.Add(entity);
    }

    public async Task<ClientMembership?> GetActiveClientMembershipByClientId(Guid clientId)
    {
        return await _dbContext.ClientMemberships.FirstOrDefaultAsync(item => item.ClientId == clientId && item.IsActive);
    }

    public async Task<int> GetActiveClientMembershipsCountAsync(DateTime? from)
    {
        IQueryable<ClientMembership> query =
            _dbContext.ClientMemberships.Where(x => x.IsActive);

        if (from != null)
        {
            DateTime start = from.Value.Date; // 00:00
            DateTime end = start.AddDays(1);            

            query = query.Where(x => x.StartDate >= start && x.StartDate < end);
        }

        return await query.CountAsync();
    }

    public async Task<List<PointResponse>> GetAllClientMembershipsOverTime()
    {
        DateTime startDate = DateTime.UtcNow.Date - TimeSpan.FromDays(6);
        DateTime endDate = DateTime.UtcNow.Date;

        List<PointResponse> points = await _dbContext.ClientMemberships.Where(item => item.StartDate >= startDate && item.StartDate.Date <= endDate).GroupBy(item => item.StartDate.Date).Select(item => new PointResponse()
        {
            Date = item.Key,
            TimeSeriesPoint = item.Count()
        }).ToListAsync();

        return points;   
    }

    public async Task<PageResult<ClientMembershipResponse>> GetAllAsync(int page = 1, int pageSize = 50, string? searchText = null)
    {
        IQueryable<ClientMembership> query = _dbContext.ClientMemberships;
        int totalCount = await query.CountAsync();
        int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        List<ClientMembershipResponse> items = await query.Skip((page - 1) * pageSize).Take(pageSize).Select(item => item.ToClientMembershipResponse())
        .ToListAsync();
        if (searchText == null)
        {
            return new PageResult<ClientMembershipResponse>()
            {
                Items = items,
                TotalCount = totalCount,
                TotalPages = totalPages,
                PageSize = pageSize,
                CurrentPage = page
            };
        }

        searchText = searchText.ToLower();
        string[] terms = searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string term in terms)
        {
            string pattern = $"%{term}%";
            query = query.Where(item => item.Client!.FirstName.ToLower().Contains(term) ||
                                            item.Client.LastName.ToLower().Contains(term) ||
                                                item.Client.Email.ToLower().Contains(term) ||
                                                    item.Client.PhoneNumber.ToLower().Contains(term) ||
                                                        item.Membership!.Name.ToLower().Contains(term));
        }

        int filteredTotalCount = await query.CountAsync();
        int filteredTotalPages = (int)Math.Ceiling(filteredTotalCount / (double)pageSize);

        return new PageResult<ClientMembershipResponse>()
        {
            Items = await query.Skip((page - 1) * pageSize).Take(pageSize).Select(item => item.ToClientMembershipResponse()).ToListAsync(),
            TotalCount = filteredTotalCount,
            TotalPages = filteredTotalPages,
            PageSize = pageSize,
            CurrentPage = page
        };

    }

    public async Task<IEnumerable<ClientMembershipResponse>> GetAllClientMemberships(Guid id)
    {
        return await _dbContext.ClientMemberships.Where(item => item.ClientId == id).Select(item => new ClientMembershipResponse
        {
            Id = item.Id,
            Name = item.Membership!.Name,
            MembershipType = item.Membership.MembershipType.ToString(),
            IsActive = item.IsActive,
            StartDate = item.StartDate.ToString("yyyy.MM.dd"),
            EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToString("yyyy.MM.dd") : "Permanent",
            CreatedAt = item.StartDate,
            DeletedAt = item.DeletedAt
        }).ToListAsync();
    }

    public async Task<ClientMembership?> GetByIdAsync(Guid id)
    {
        return await _dbContext.ClientMemberships.Include(item => item.Termination)
                                                    .Include(item => item.Contract)
                                                        .Include(item => item.Membership)
                                                            .Include(item => item.Client)
                                                                .FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<ClientMembership?> UpdateAsync(Guid id, ClientMembership entity)
    {
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<ClientMembership?> GetActiveClientMembershipById(Guid clientMembershipId)
    {
        return await _dbContext.ClientMemberships.FirstOrDefaultAsync(item => item.IsActive && item.Id == clientMembershipId);
    }
    public async Task<ClientMembershipWebResponse?> GetClientMembershipByClientIdAsync(Guid clientId)
    {
        return await _dbContext.ClientMemberships.Where(item => item.ClientId == clientId && item.IsActive).Select(item => new ClientMembershipWebResponse()
        {
            EndDate = item.EndDate.HasValue ? item.EndDate.Value.ToString("dd.MM.yyyy") : "Indefinite time",
            Name = item.Membership.Name,
            StartDate = item.StartDate.ToString("dd.MM.yyyy"),
            IsActive = item.IsActive,
        }).FirstOrDefaultAsync();
    }
}
