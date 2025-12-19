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
    public async Task<MembershipFeature?> GetMembershipFeatureByMembershipIdAndFeatureId(Guid featureId, Guid membershipId)
    {
        return await _dbContext.MembershipFeatures.FirstOrDefaultAsync(item => item.FeatureId == featureId && item.MembershipId == membershipId);
    }

    public async Task<IEnumerable<MembershipFeature>> GetMembershipFeaturesByMembershipId(Guid membershipId)
    {
        return await _dbContext.MembershipFeatures.Where(item => item.MembershipId == membershipId).Include(item => item.Feature).ToListAsync();
    }
}
