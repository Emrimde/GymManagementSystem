using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class MembershiPriceService : IMembershipPriceService
{
    private readonly IMembershipPriceRepository _membershipPriceRepository;
    public MembershiPriceService(IMembershipPriceRepository membershipPriceRepository)
    {
        _membershipPriceRepository = membershipPriceRepository;
    }
    public async Task<Result<IEnumerable<MembershipPriceResponse>>> GetMembershipPricesByMembershipIdAsync(Guid membershipId)
    {
        IEnumerable<MembershipPriceResponse> membershipPrices = await _membershipPriceRepository.GetMembershipPricesByMembershipId(membershipId);
        return Result<IEnumerable<MembershipPriceResponse>>.Success(membershipPrices);

    }
}
