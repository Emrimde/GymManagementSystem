using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.MembershipPrice;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IMembershipPriceRepository
{
    Task<IEnumerable<MembershipPriceResponse>> GetMembershipPricesByMembershipId(Guid membershipId);
    Task<MembershipPrice?> GetActiveMembershipPriceByMembershipId(Guid membershipId);
    void AddMembershipPrice(MembershipPrice membershipPrice);
    void EditMembershipPrice(MembershipPrice membershipPrice);
}
