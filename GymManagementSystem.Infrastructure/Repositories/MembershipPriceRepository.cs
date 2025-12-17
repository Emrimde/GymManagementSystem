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



    public async Task<IEnumerable<MembershipPriceResponse>> GetMembershipPricesByMembershipId(Guid membershipId)
    {
        return await _dbContext.MembershipPrices.Where(item => item.MembershipId == membershipId).Select(item => new MembershipPriceResponse
        {
            Price = item.Price,
            ValidFrom = item.ValidFrom,
            ValidTo = item.ValidTo,
            LabelPrice = item.LabelPrice
        }).ToListAsync();
    }

}
