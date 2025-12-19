using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.MembershipFeature;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IMembershipFeatureRepository
{
    void AddMembershipFeature(MembershipFeature membershipFeature);
    Task<MembershipFeature?> GetMembershipFeatureByMembershipIdAndFeatureId(Guid featureId, Guid membershipId);
    Task<IEnumerable<MembershipFeature>> GetMembershipFeaturesByMembershipId(Guid membershipId);
}
