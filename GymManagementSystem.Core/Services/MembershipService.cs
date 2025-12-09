using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class MembershipService<Entity> : IService<MembershipResponse, MembershipAddRequest, MembershipUpdateRequest, Membership>
{
    private readonly IRepository<Membership> _repository;
    public MembershipService(IRepository<Membership> repository)
    {
        _repository = repository;
    }

    public async Task<Result<MembershipResponse>> CreateAsync(MembershipAddRequest entity)
    {
        Membership membership = entity.ToMembership();

        Membership response = await _repository.CreateAsync(membership);
       
        return Result<MembershipResponse>.Success(response.ToMembershipResponse());
    }

    public async Task<Result<IEnumerable<MembershipResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
       IEnumerable<Membership> memberships =  await _repository.GetAllAsync();
       IEnumerable<MembershipResponse> response =  memberships.Select(item => item.ToMembershipResponse());
       return Result<IEnumerable<MembershipResponse>>.Success(response);
    }

    public async Task<Result<MembershipResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Membership? membership = await _repository.GetByIdAsync(id, cancellationToken);
        if (membership == null)
        {
            return Result<MembershipResponse>.Failure($"Membership with id {id} not found");
        }
        return Result<MembershipResponse>.Success(membership.ToMembershipResponse());
    }

    public async Task<Result<MembershipResponse>> UpdateAsync(Guid id, MembershipUpdateRequest entity)
    {
        Membership? membership = await _repository.UpdateAsync(id, entity.ToMembership());
        if (membership == null)
        {
            return Result<MembershipResponse>.Failure($"Membership with id {id} not found");
        }
        return Result<MembershipResponse>.Success(membership.ToMembershipResponse());
    }
}
