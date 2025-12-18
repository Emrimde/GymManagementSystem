using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class MembershipPriceService : IMembershipPriceService
{
    private readonly IMembershipPriceRepository _membershipPriceRepository;
    private readonly IUnitOfWork _unitOfWork;
    public MembershipPriceService(IMembershipPriceRepository membershipPriceRepository, IUnitOfWork unitOfWork)
    {
        _membershipPriceRepository = membershipPriceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> CreateMembershipPriceAsync(MembershipPriceAddRequest membershipPriceAddRequest)
    {
        MembershipPrice membershipPrice = membershipPriceAddRequest.ToMembershipPrice();
        membershipPrice.ValidFrom = DateTime.UtcNow;

        if(membershipPrice.LabelPrice == null)
        {
            membershipPrice.LabelPrice = "Regular";
        }

        MembershipPrice? activeMembershipPrice = await _membershipPriceRepository.GetActiveMembershipPriceByMembershipId(membershipPriceAddRequest.MembershipId);

        if(activeMembershipPrice == null)
        {
            return Result<Unit>.Failure("Membership doesn't have actual price", StatusCodeEnum.InternalServerError);
        }

        activeMembershipPrice.ValidTo = DateTime.UtcNow;

        _membershipPriceRepository.AddMembershipPrice(membershipPrice);
        _membershipPriceRepository.EditMembershipPrice(activeMembershipPrice);
        await _unitOfWork.SaveChangesAsync();

        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<MembershipPriceResponse>>> GetMembershipPricesByMembershipIdAsync(Guid membershipId)
    {
        IEnumerable<MembershipPrice> membershipPrices = await _membershipPriceRepository.GetMembershipPricesByMembershipId(membershipId);
        IEnumerable<MembershipPriceResponse> dto = membershipPrices.Select(item => item.ToMembershipPriceResponse());
        return Result<IEnumerable<MembershipPriceResponse>>.Success(dto, StatusCodeEnum.Ok);
    }
}
