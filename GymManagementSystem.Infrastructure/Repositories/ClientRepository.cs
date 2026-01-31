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

    public void CreateAsync(Client entity)
    {
        _dbContext.Clients.Add(entity);
    }

    public async Task<PageResult<ClientResponse>> GetAllAsync(bool? isActive, int pageSize = 50, int page = 1,string? searchText = null)
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

        if (isActive.HasValue)
        {
            query = query.Where(item => item.IsActive == isActive);
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

    public async Task<IEnumerable<ClientContactResponse>> GetClientContactsAsync()
    {
        return await _dbContext.Clients.AsNoTracking().Select(item => new ClientContactResponse()
        {
            Email = item.Email,
            PhoneNumber = item.PhoneNumber
        }).ToListAsync();
    } 

    public async Task<Client?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Clients
    .Include(item => item.ClientMemberships)
        .ThenInclude(item => item.Membership)
    .FirstOrDefaultAsync(item => item.Id == id);
    }


    public async Task<ClientDetailsResponse?> GetClientDetailsAsync(Guid clientId)
    {
        return await _dbContext.Clients
            .Where(item => item.Id == clientId)
            .Select(item => new ClientDetailsResponse
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                DateOfBirth = item.DateOfBirth.ToString("dd.MM.yyyy"),
                Street = item.StreetAddress,
                City = item.City,
                IsActive = item.IsActive,

                ClientMembershipName = item.ClientMemberships
                    .Where(item => item.IsActive)
                    .Select(item => item.Membership!.Name + " " + item.Membership.MembershipType.ToString())
                    .FirstOrDefault(),

                CanTerminate = item.ClientMemberships
                    .Any(item =>
                        item.IsActive &&
                        item.Termination == null
                    ),

                TotalVisits = item.Visits.Count,

                LastVisitDate = item.Visits
                    .OrderByDescending(item => item.VisitDate)
                    .Select(item => item.VisitDate.ToString("dd.MM.yyyy"))
                    .FirstOrDefault(),

                Valid = item.ClientMemberships.Where(item => item.IsActive).Select(item => item.StartDate.ToString("dd.MM.yyyy") + " - " + (item.EndDate.HasValue ? item.EndDate.Value.ToString("dd.MM.yyyy") : "Indefinite"))
               .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
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
                MembershipId = item.ClientMemberships
                    .Where(item => item.IsActive)
                    .Select(item => (Guid?)item.MembershipId)
                    .FirstOrDefault(),
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
