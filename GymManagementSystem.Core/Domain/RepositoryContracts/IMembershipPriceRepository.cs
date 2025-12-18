using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IMembershipPriceRepository
{
    Task<IEnumerable<MembershipPrice>> GetMembershipPricesByMembershipId(Guid membershipId);
    Task<MembershipPrice?> GetActiveMembershipPriceByMembershipId(Guid membershipId);
    void AddMembershipPrice(MembershipPrice membershipPrice);
    void EditMembershipPrice(MembershipPrice membershipPrice);
}
