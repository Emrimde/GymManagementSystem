using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class ClientMembershipRepository : IRepository<ClientMembership>
{
    private readonly ApplicationDbContext _dbContext;
    public ClientMembershipRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ClientMembership> CreateAsync(ClientMembership entity, CancellationToken cancellationToken)
    {
        entity.IsActive = true;
        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;
        _dbContext.ClientMemberships.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<ClientMembership>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.ClientMemberships.Include(item => item.Client).Include(item => item.Membership).ToListAsync(cancellationToken);
    }

    public async Task<ClientMembership?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.ClientMemberships.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public async Task<ClientMembership?> UpdateAsync(Guid id, ClientMembership entity, CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }
}
