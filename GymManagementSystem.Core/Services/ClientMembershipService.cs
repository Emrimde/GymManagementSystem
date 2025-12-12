using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ClientMembershipService : IClientMembershipService
{
    private readonly IClientMembershipRepository _clientMembershipRepository;
    private readonly IRepository<ContractResponse,Contract> _contractRepo;
    private readonly IMembershipRepository _membershipRepo;

    public ClientMembershipService(IClientMembershipRepository clientMembershipRepository, IRepository<ContractResponse,Contract> contractRepo, IMembershipRepository membershipRepo)
    {
        _clientMembershipRepository = clientMembershipRepository;
        _contractRepo = contractRepo;
        _membershipRepo = membershipRepo;
    }
    public async Task<Result<ClientMembershipInfoResponse>> CreateAsync(ClientMembershipAddRequest entity)
    {
        ClientMembership clientMembership = entity.ToClientMembership();
        Membership? membership = await _membershipRepo.GetByIdAsync(entity.MembershipId);
        if(membership == null)
        {
            return Result<ClientMembershipInfoResponse>.Failure("Membership not found");
        }
        if(membership.IsTrainerOnly)
        {
            return Result<ClientMembershipInfoResponse>.Failure("Cannot create client membership for trainer only membership");
        }
        if(membership.MembershipType == MembershipTypeEnum.Annual)
        {
            clientMembership.EndDate = clientMembership.StartDate.AddYears(1);
        }
        if(clientMembership.StartDate > DateTime.UtcNow)
        {
            clientMembership.MembershipStatus = MembershipStatusEnum.Upcoming;
        }
        else
        {
            clientMembership.MembershipStatus = MembershipStatusEnum.Active;
        }

            ClientMembership addedClientMembership = await _clientMembershipRepository.CreateAsync(clientMembership);
        Contract contract = new Contract()
        {
            ClientMembershipId = addedClientMembership.Id,
            CreatedAt = addedClientMembership.CreatedAt,
            ContractStatus = ContractStatus.Draft,
            IsActive = true
        };
        Contract createdContract = await _contractRepo.CreateAsync(contract);
        return Result<ClientMembershipInfoResponse>.Success(addedClientMembership.ToClientMembershipInfoResponse(createdContract.Id));
    }

    public async Task<PageResult<ClientMembershipResponse>> GetAllAsync(string? searchText, int pageSize = 50, int page = 1)
    { 
        PageResult<ClientMembershipResponse> clientMembershipList = await _clientMembershipRepository.GetAllAsync(pageSize: pageSize, page:page, searchText: searchText);
        return clientMembershipList;
    }

    public async Task<Result<IEnumerable<ClientMembershipResponse>>> GetAllMembershipsClientHistoryAsync(Guid id)
    {
        IEnumerable<ClientMembershipResponse> clientMemberships = await _clientMembershipRepository.GetAllClientMemberships(id);
        return Result<IEnumerable<ClientMembershipResponse>>.Success(clientMemberships);
    }

    public Task<Result<ClientMembershipResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ClientMembershipResponse>> UpdateAsync(Guid id, ClientMembershipUpdateRequest entity)
    {
        throw new NotImplementedException();
    }
}
