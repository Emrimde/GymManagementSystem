using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class MembershipPriceService : IMembershipPriceService
{
    private readonly IMembershipPriceRepository _membershipPriceRepository;
    public MembershipPriceService(IMembershipPriceRepository membershipPriceRepository)
    {
        _membershipPriceRepository = membershipPriceRepository;
    }
    public async Task<Result<IEnumerable<MembershipPriceResponse>>> GetMembershipPricesByMembershipIdAsync(Guid membershipId)
    {
        IEnumerable<MembershipPriceResponse> result = await _membershipPriceRepository.GetMembershipPricesByMembershipId(membershipId);
        return Result<IEnumerable<MembershipPriceResponse>>.Success(result, StatusCodeEnum.Ok);
    }
}
