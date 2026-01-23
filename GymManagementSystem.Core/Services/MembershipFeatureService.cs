using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;
public class MembershipFeatureService : IMembershipFeatureService
{
    private readonly IMembershipFeatureRepository _membershipFeatureRepo;
    private readonly IUnitOfWork _unitOfWork;
    public MembershipFeatureService(IMembershipFeatureRepository membershipFeature, IUnitOfWork unitOfWork)
    {
        _membershipFeatureRepo = membershipFeature;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> CreateMembershipFeatureAsync(MembershipFeatureAddRequest request)
    {
        MembershipFeature membershipFeature = request.ToMembershipFeature();

        _membershipFeatureRepo.AddMembershipFeature(membershipFeature);
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<MembershipFeatureResponse>>> GetMembershipFeaturesByMembershipIdAsync(Guid membershipId)
    {
        IEnumerable<MembershipFeature> membershipFeatures = await _membershipFeatureRepo.GetMembershipFeaturesByMembershipId(membershipId);
        return Result<IEnumerable<MembershipFeatureResponse>>.Success(membershipFeatures.Select(item => item.ToMembershipFeatureResponse()), StatusCodeEnum.Ok);
    }
}
