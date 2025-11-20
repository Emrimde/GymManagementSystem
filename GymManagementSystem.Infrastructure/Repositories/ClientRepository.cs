using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class ClientRepository : IRepository<Client>
{
    private readonly ApplicationDbContext _dbContext;

    public ClientRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Client> CreateAsync(Client entity, CancellationToken cancellationToken)
    {
        _dbContext.Clients.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
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

    public async Task<Client?> UpdateAsync(Guid id, Client entity, CancellationToken cancellationToken)
    {
        Client? client = await _dbContext.Clients.FirstOrDefaultAsync(item => item.Id == id,cancellationToken);

        if (client == null)
        { 
            return null;
        }

        client.ModifyClient(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return client;
    }
}
