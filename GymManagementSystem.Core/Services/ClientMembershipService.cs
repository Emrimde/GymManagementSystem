using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ClientMembershipService<Entity> : IClientMembershipService
{
    private readonly IRepository<ClientMembership> _clientMembershipRepository;
    private readonly IRepository<Contract> _contractRepo;
    public ClientMembershipService(IRepository<ClientMembership> clientMembershipRepository, IRepository<Contract> contractRepo)
    {
        _clientMembershipRepository = clientMembershipRepository;
        _contractRepo = contractRepo;
    }
    public async Task<Result<ClientMembershipInfoResponse>> CreateAsync(ClientMembershipAddRequest entity)
    {
        ClientMembership clientMembership = entity.ToClientMembership();
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

    public async Task<Result<IEnumerable<ClientMembershipResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<ClientMembership> clientMembershipList = await _clientMembershipRepository.GetAllAsync(cancellationToken);
        return Result<IEnumerable<ClientMembershipResponse>>.Success(clientMembershipList.Select(item => item.ToClientMembershipResponse()));
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
