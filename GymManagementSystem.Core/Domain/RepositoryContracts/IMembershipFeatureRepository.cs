using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.MembershipFeature;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IMembershipFeatureRepository
{
    void AddMembershipFeature(MembershipFeature membershipFeature);
    Task<MembershipFeatureForEditResponse?> GetMembershipFeatureForEditByIdAsync(Guid membershipFeatureId);
    Task<MembershipFeature?> GetMembershipFeatureByIdAsync(Guid membershipFeatureId);
    Task<IEnumerable<MembershipFeature>> GetMembershipFeaturesByMembershipId(Guid membershipId);
    bool MarkForHardDelete(Guid membershipFeatureId);
}
