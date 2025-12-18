using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IMembershipFeatureRepository
{
    void AddMembershipFeature(MembershipFeature membershipFeature);
    Task<MembershipFeature?> GetMembershipFeatureByMembershipIdAndFeatureId(Guid featureId, Guid membershipId);
}
