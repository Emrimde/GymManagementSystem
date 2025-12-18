using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using System.Runtime.InteropServices;

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
        if (((membershipFeature.BenefitFrequency == null || membershipFeature.BenefitFrequency == 0) && membershipFeature.Period != PeriodEnum.None)
            || ((membershipFeature.BenefitFrequency != null && membershipFeature.Period == PeriodEnum.None)))
        {
            return Result<Unit>.Failure("If you set frequency you have to set period and vice versa", StatusCodeEnum.BadRequest);
        }

        MembershipFeature? existingMembershipFeature = await _membershipFeatureRepo.GetMembershipFeatureByMembershipIdAndFeatureId(request.FeatureId, request.MembershipId);
        if( existingMembershipFeature != null)
        {
            return Result<Unit>.Failure("The feature is in membership", StatusCodeEnum.BadRequest);
        }

        _membershipFeatureRepo.AddMembershipFeature(membershipFeature);
        await _unitOfWork.SaveChangesAsync();
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }
}
