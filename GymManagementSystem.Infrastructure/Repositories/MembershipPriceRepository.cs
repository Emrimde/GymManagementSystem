using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class MembershipPriceRepository : IMembershipPriceRepository
{
    private readonly ApplicationDbContext _dbContext;
    public MembershipPriceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddMembershipPrice(MembershipPrice membershipPrice)
    {
        _dbContext.MembershipPrices.Add(membershipPrice);
    }

    public void EditMembershipPrice(MembershipPrice membershipPrice)
    {
       _dbContext.MembershipPrices.Update(membershipPrice);
    }

    public async Task<MembershipPrice?> GetActiveMembershipPriceByMembershipId(Guid membershipId)
    {
        return await _dbContext.MembershipPrices.FirstOrDefaultAsync(item => item.MembershipId == membershipId && item.ValidTo == null);
    }

    public async Task<IEnumerable<MembershipPrice>> GetMembershipPricesByMembershipId(Guid membershipId)
    {
        return await _dbContext.MembershipPrices.Where(item => item.MembershipId == membershipId).ToListAsync();
    }
}
