using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class MembershipRepository : IRepository<Membership>
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

    public async Task<IEnumerable<Membership>> GetAllAsync(string? searchText = null)
    {
        return await _dbContext.Memberships.ToListAsync();
    }

    public async Task<Membership?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
       return await _dbContext.Memberships.FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
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
        membership.IsTrainerOnly = entity.IsTrainerOnly;
        await _dbContext.SaveChangesAsync();
        return membership;
    }
}
