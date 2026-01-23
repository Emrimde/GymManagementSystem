using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.Membership;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class MembershipRepository : IMembershipRepository
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

    public async Task<IEnumerable<MembershipResponse>> GetAllMemberships()
    {
        IQueryable<Membership> query = _dbContext.Memberships;
        return await query.Include(item => item.MembershipPrices).Select(item => item.ToMembershipResponse()).ToListAsync();
    }

    public async Task<IEnumerable<MembershipWebDetailsResponse>> GetAllMembershipsWithFeaturesAsync()
    {
        return await _dbContext.Memberships.Select(item => new MembershipWebDetailsResponse()
        {
            Id = item.Id,
            IsMonthly = item.MembershipType == Core.Enum.MembershipTypeEnum.Monthly ? true : false,
            MembershipFeatures = item.MembershipFeatures.Select(item => item.FeatureDescription).ToList(),
            MembershipName = item.Name,
            Price = item.MembershipPrices.Where(item => item.ValidTo == null).Select(item => item.Price).FirstOrDefault(),
        }).ToListAsync();
    }

    public async Task<Membership?> GetByIdAsync(Guid id)
    {
       return await _dbContext.Memberships.Include(item => item.MembershipPrices).FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<MembershipInfoResponse?> GetMembershipNameAsync(Guid membershipId)
    {
       return await _dbContext.Memberships.Where(item => item.Id == membershipId).Select(item => new MembershipInfoResponse()
        {
            MembershipId = membershipId,
            MembershipName = item.Name,
        }).FirstOrDefaultAsync();
    }

    public async Task<Membership?> UpdateAsync(Guid id, Membership entity)
    {
        Membership? membership =  await _dbContext.Memberships.FirstOrDefaultAsync(item => item.Id == id);

        if (membership == null)
        {
            return null!;
        }

        membership.Name = entity.Name;
        await _dbContext.SaveChangesAsync();
        return membership;
    }
}
