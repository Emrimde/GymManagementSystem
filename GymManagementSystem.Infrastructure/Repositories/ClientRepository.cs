using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.WebDTO;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ClientRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Client> CreateAsync(Client entity)
    {
        _dbContext.Clients.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<PageResult<ClientResponse>> GetAllAsync( int pageSize = 50, int page = 1,string? searchText = null)
    {
        IQueryable<Client> query = _dbContext.Clients;

        if (searchText != null)
        {
            string searchTextlower = searchText.ToLower();
            string[] terms = searchTextlower.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string term in terms)
            {
                string pattern = $"%{term}%";
                query = query.Where(item => item.FirstName.ToLower().Contains(term) ||
                                                item.LastName.ToLower().Contains(term) ||
                                                    item.PhoneNumber.Contains(term) ||
                                                        item.Email.ToLower().Contains(term));
            }
        }


    
        int totalCount = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        List<ClientResponse> list  = await query.OrderBy(item => item.FirstName)
                                                    .Skip((page - 1) * pageSize)
                                                        .Take(pageSize)
                                                            .Select(item => item.ToClientResponse())
                                                                .ToListAsync();
        
    

        return new PageResult<ClientResponse>()
        {
            Items = list,
            TotalCount = totalCount,
            PageSize = pageSize,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Clients
    .Include(c => c.ClientMemberships)
        .ThenInclude(cm => cm.Membership)
    .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Client?> UpdateAsync(Guid id, Client entity)
    {
        Client? client = await _dbContext.Clients.FirstOrDefaultAsync(item => item.Id == id);

        if (client == null)
        {
            return null;
        }

        client.ModifyClient(entity);
        await _dbContext.SaveChangesAsync();
        return client;
    }

    public async Task<IEnumerable<Client>> LookUpClientsAsync(string query, Guid? scheduledClassId = null)
    {
        if (scheduledClassId != null)
        {
            return await _dbContext.Clients
                .Include(item => item.ClassBookings)
                .Include(item => item.ClientMemberships)
                .AsNoTracking()
                .Where(item =>
                    (EF.Functions.Like(item.FirstName, $"%{query}%")
                    || EF.Functions.Like(item.LastName, $"%{query}%"))
                    && !item.ClassBookings.Any(item => item.ScheduledClassId == scheduledClassId) &&
                    item.ClientMemberships.Any(item => item.IsActive == true))
                .OrderBy(item => item.LastName)
                .ThenBy(item => item.FirstName)
                .Take(10)
                .ToListAsync();
        }
        return await _dbContext.Clients
           .Include(item => item.ClassBookings)
           .Include(item => item.ClientMemberships)
           .AsNoTracking()
           .Where(item =>
               (EF.Functions.Like(item.FirstName, $"%{query}%")
               || EF.Functions.Like(item.LastName, $"%{query}%"))
               &&
               item.ClientMemberships.Any(item => item.IsActive == true))
           .OrderBy(item => item.LastName)
           .ThenBy(item => item.FirstName)
           .Take(10)
           .ToListAsync();
    }

    public async Task<ClientInfoResponse?> GetClientFullNameByIdAsync(Guid id)
    {
        return await _dbContext.Clients
            .Where(item => item.Id == id)
            .Select(item => new ClientInfoResponse
            {
                Id = item.Id,
                MembershipId = item.ClientMemberships.FirstOrDefault(item => item.IsActive)!.MembershipId,
                FullName = item.FirstName + " " + item.LastName
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ClientDetailsWebResponse?> GetClientProfileInfoAsync(Guid clientId)
    {
      return await _dbContext.Clients.Where(item => item.Id == clientId).Select(item => new ClientDetailsWebResponse()
      {
          Email = item.Email,
          FirstName = item.FirstName,
          PhoneNumber = item.PhoneNumber,
          LastName = item.LastName,
          MembershipName = item.ClientMemberships.Where(item => item.IsActive).Select(item => item.Membership.Name + " " + item.Membership.MembershipType.ToString()).FirstOrDefault() ?? null,
          City = item.City,
          DateOfBirth = item.DateOfBirth,
          Street = item.StreetAddress
      }).FirstOrDefaultAsync();  
    }

    public async Task UpdateClientAsync(Client updated)
    {
        Client? client = await _dbContext.Clients.FindAsync(updated.Id);
        if (client == null) return;

        client.City = updated.City;
        client.StreetAddress = updated.StreetAddress;
        client.DateOfBirth = updated.DateOfBirth;
        client.PhoneNumber = updated.PhoneNumber;
    }

   
}
