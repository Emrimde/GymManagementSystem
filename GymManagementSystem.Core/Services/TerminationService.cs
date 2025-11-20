using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;


namespace GymManagementSystem.Core.Services;

public class TerminationService : IServiceReader<TerminationResponse>, IServiceAdder<TerminationResponse, TerminationAddRequest>
{
    private readonly IRepository<Termination> _terminationRepo;
    private readonly IContractRepository _contractRepo;
    public TerminationService(IRepository<Termination> terminationRepository, IContractRepository contractRepository) 
    {
        _terminationRepo = terminationRepository;
        _contractRepo = contractRepository;
    }

    public async Task<Result<TerminationResponse>> CreateAsync(TerminationAddRequest entity, CancellationToken cancellationToken)
    {
        Contract? contract = await _contractRepo.GetByIdAsync(entity.ContractId,cancellationToken);
        if (contract == null || contract.IsActive == false) 
        {
            return Result<TerminationResponse>.Failure("Error: Termination cannot be created because client doesn't have contract", StatusCodeEnum.InternalServerError);
        }

        if (contract.ContractStatus != ContractStatus.Signed)
        {
            return Result<TerminationResponse>.Failure("Error: Terminantion cannot be created because client either has not signed contract or the contract is already terminated", StatusCodeEnum.InternalServerError);
        }
        contract.ContractStatus = ContractStatus.Terminated;
        if (contract.ClientMembership != null)
        {
            contract.ClientMembership.IsActive = false;
        }

        await _contractRepo.UpdateAsync(contract.Id,contract,cancellationToken);
        Termination termination = entity.ToTermination();
        //contract.c = true; // czy to nie powinien robic repo contract?
        //termination.ContractId = contract.Id; // to tez?

        Termination createdTermination = await _terminationRepo.CreateAsync(termination,cancellationToken);
        
        return Result<TerminationResponse>.Success(createdTermination.ToTerminationResponse(), StatusCodeEnum.Ok);
    }

    public async Task<Result<IEnumerable<TerminationResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Termination> terminations  = await _terminationRepo.GetAllAsync(cancellationToken);
        IEnumerable<TerminationResponse> terminationsResponse = terminations.Select(item => item.ToTerminationResponse());
        return Result<IEnumerable<TerminationResponse>>.Success(terminationsResponse, StatusCodeEnum.Ok);
    }

    public Task<Result<TerminationResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
