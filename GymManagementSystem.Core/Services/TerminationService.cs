using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;


namespace GymManagementSystem.Core.Services;

public class TerminationService : ITerminationService
{
    private readonly IRepository<TerminationResponse,Termination> _terminationRepo;
    private readonly IContractRepository _contractRepo;
    public TerminationService(IRepository<TerminationResponse,Termination> terminationRepository, IContractRepository contractRepository) 
    {
        _terminationRepo = terminationRepository;
        _contractRepo = contractRepository;
    }

    public async Task<Result<TerminationResponse>> CreateAsync(TerminationAddRequest entity)
    {
        Contract? contract = await _contractRepo.GetByIdAsync(entity.ContractId);
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

        await _contractRepo.UpdateAsync(contract.Id,contract);
        Termination termination = entity.ToTermination();
        //contract.c = true; // czy to nie powinien robic repo contract?
        //termination.ContractId = contract.Id; // to tez?

        Termination createdTermination = await _terminationRepo.CreateAsync(termination);
        
        return Result<TerminationResponse>.Success(createdTermination.ToTerminationResponse(), StatusCodeEnum.Ok);
    }

    public async Task<PageResult<TerminationResponse>> GetAllAsync()
    {
        PageResult<TerminationResponse> terminations  = await _terminationRepo.GetAllAsync();
        return terminations;
    }

    public Task<Result<TerminationResponse>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
