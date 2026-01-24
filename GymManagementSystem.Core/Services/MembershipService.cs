using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.Membership;

namespace GymManagementSystem.Core.Services;

public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    public MembershipService(IMembershipRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MembershipResponse>> CreateAsync(MembershipAddRequest entity)
    {
        Membership membership = entity.ToMembership();

        Membership response = await _repository.CreateAsync(membership);
       
        return Result<MembershipResponse>.Success(response.ToMembershipResponse());
    }

    public async Task<Result<IEnumerable<MembershipResponse>>> GetAllAsync()
    {
       IEnumerable<MembershipResponse> memberships =  await _repository.GetAllMemberships();
       return Result<IEnumerable<MembershipResponse>>.Success(memberships,StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<MembershipWebDetailsResponse>>> GetAllMembershipsWithFeaturesAsync()
    {
        IEnumerable<MembershipWebDetailsResponse> dto = await _repository.GetAllMembershipsWithFeaturesAsync();
        return Result<IEnumerable<MembershipWebDetailsResponse>>.Success(dto, StatusCodeEnum.Ok);
    }

    public async Task<Result<MembershipResponse>> GetByIdAsync(Guid id)
    {
        Membership? membership = await _repository.GetByIdAsync(id);
        if (membership == null)
        {
            return Result<MembershipResponse>.Failure($"Membership with id {id} not found");
        }
        return Result<MembershipResponse>.Success(membership.ToMembershipResponse());
    }

    public async Task<Result<MembershipInfoResponse>> GetMembershipNameAsync(Guid membershipId)
    {
        MembershipInfoResponse? dto = await _repository.GetMembershipNameAsync(membershipId);
        if (dto == null)
        {
            return Result<MembershipInfoResponse>.Failure("Error during loading membership name", StatusCodeEnum.InternalServerError);
        }
        return Result<MembershipInfoResponse>.Success(dto, StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> UpdateAsync(Guid id, MembershipUpdateRequest entity)
    {
        Membership? membership = await _repository.GetByIdAsync(id);
        if (membership == null)
        {
            return Result<Unit>.Failure($"Membership with id {id} not found");
        }
        membership.ModifyMembership(entity);
        await _unitOfWork.SaveChangesAsync();

        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.Ok);
    }
}
