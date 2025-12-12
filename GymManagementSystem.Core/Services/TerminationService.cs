using GymManagementSystem.Core.Domain;
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
    private readonly IUnitOfWork _unitOfWork;
    public TerminationService(IRepository<TerminationResponse,Termination> terminationRepository, IContractRepository contractRepository,IUnitOfWork unitOfWork) 
    {
        _terminationRepo = terminationRepository;
        _contractRepo = contractRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TerminationResponse>> CreateAsync(TerminationAddRequest entity)
    {
        Contract? contract = await _contractRepo.GetContractByClientMembershipIdAsync(entity.ClientMembershipId);
        if (contract == null) 
        {
            return Result<TerminationResponse>.Failure("Error: Termination cannot be created because client doesn't have contract", StatusCodeEnum.InternalServerError);
        }

        if (contract.ContractStatus != ContractStatus.Signed)
        {
            return Result<TerminationResponse>.Failure("Error: Terminantion cannot be created because client either has not signed contract or the contract is already terminated", StatusCodeEnum.InternalServerError);
        }
        contract.ContractStatus = ContractStatus.Terminated;

        if (contract.ClientMembership == null)
        {
            return Result<TerminationResponse>.Failure("Error: Termination cannot be created because client membership not found", StatusCodeEnum.InternalServerError);
        }

        contract.ClientMembership.IsActive = false;
        await _contractRepo.UpdateAsync(contract.Id,contract);
        Termination termination = entity.ToTermination();
        Termination createdTermination = await _terminationRepo.CreateAsync(termination);
        //await _unitOfWork.SaveChangesAsync(); to powinno byc 
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
