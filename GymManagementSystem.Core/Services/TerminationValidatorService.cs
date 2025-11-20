using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class TerminationValidatorService : ITerminationValidator
{
    private readonly IContractRepository _contractRepo;
    public TerminationValidatorService(IContractRepository contractRepo)
    {
        
        _contractRepo = contractRepo;
    }
    public async Task<Result<bool>> CanCreateTerminationAsync(Guid clientId)
    {
        Contract? contract = await _contractRepo.GetActiveContractAsync(clientId);
        if (contract == null) 
        {
            return Result<bool>.Failure("Client doesn't have a contract right now", StatusCodeEnum.NotFound);
        }
        if(contract.ContractStatus != ContractStatus.Signed)
        {
            return Result<bool>.Failure("Client didn't signed a contract", StatusCodeEnum.BadRequest);
        }
        return Result<bool>.Success(true);
    }
}
