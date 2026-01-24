using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class MembershipFeatureRepository : IMembershipFeatureRepository
{
    private readonly ApplicationDbContext _dbContext;
    public MembershipFeatureRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddMembershipFeature(MembershipFeature membershipFeature)
    {
        _dbContext.Add(membershipFeature);
    }

    public async Task<MembershipFeature?> GetMembershipFeatureByIdAsync(Guid membershipFeatureId)
    {
        return await _dbContext.MembershipFeatures.FirstOrDefaultAsync(item => item.Id == membershipFeatureId);
    }

    public async Task<MembershipFeatureForEditResponse?> GetMembershipFeatureForEditByIdAsync(Guid membershipId)
    {
        return await _dbContext.MembershipFeatures.Where(item => item.MembershipId == membershipId).Select(item => new MembershipFeatureForEditResponse()
        {
            FeatureDescription = item.FeatureDescription,
        }).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<MembershipFeature>> GetMembershipFeaturesByMembershipId(Guid membershipId)
    {
        return await _dbContext.MembershipFeatures.Where(item => item.MembershipId == membershipId).ToListAsync();
    }

    public bool MarkForHardDelete(Guid membershipFeatureId)
    {
        MembershipFeature? membershipFeature = _dbContext.MembershipFeatures.Find(membershipFeatureId);
        if (membershipFeature == null)
        {
            return false;
        }
        _dbContext.MembershipFeatures.Remove(membershipFeature);
        return true;
    }
}
