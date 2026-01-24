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

    public async Task<IEnumerable<MembershipPriceResponse>> GetMembershipPricesByMembershipId(Guid membershipId)
    {
        return await _dbContext.MembershipPrices.Where(item => item.MembershipId == membershipId).OrderByDescending(item => item.ValidFrom).Select(item => new MembershipPriceResponse()
        {
            LabelPrice = item.LabelPrice ?? "Regular",
            Price = item.Price,
            ValidFromLabel = item.ValidFrom.ToString("dd.MM.yyyy - HH:mm"),
            ValidToLabel = item.ValidTo.HasValue
                            ? item.ValidTo.Value.ToString("dd.MM.yyyy - HH:mm")
                            : "Active price"
        }).ToListAsync();
    }
}
