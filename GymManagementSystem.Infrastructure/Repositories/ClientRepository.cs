using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
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

    public async Task<IEnumerable<Client>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Clients.Include(item => item.ClientMemberships).ToListAsync(cancellationToken);
    }

    public async Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Clients
    .Include(c => c.ClientMemberships)
        .ThenInclude(cm => cm.Membership)
    .Include(c => c.ClientMemberships)
        .ThenInclude(cm => cm.Contract)
    .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
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

    public async Task<IEnumerable<Client>> LookUpClientsAsync(string query, Guid scheduledClassId)
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

}
