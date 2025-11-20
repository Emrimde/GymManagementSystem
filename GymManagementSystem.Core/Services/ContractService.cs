using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ContractService<Entity> : IContractService
{
    private readonly IContractRepository _repository;
    public ContractService(IContractRepository repository)
    {
        _repository = repository;
    }

    public Task<Result<ContractResponse>> CreateAsync(ContractAddRequest entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<ContractResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Contract> contracts = await _repository.GetAllAsync(cancellationToken);
        IEnumerable<ContractResponse> contractsResponse = contracts.Select(item => item.ToContractResponse());
        return Result<IEnumerable<ContractResponse>>.Success(contractsResponse, StatusCodeEnum.Ok);
    }

    public async Task<Result<ContractDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        Contract? contract = await _repository.GetByIdAsync(id, cancellationToken);
        if (contract == null)
        {
            return Result<ContractDetailsResponse>.Failure("Contract not found", StatusCodeEnum.NotFound);
        }
        return Result<ContractDetailsResponse>.Success(contract.ToContractDetails(), StatusCodeEnum.Ok);
    }

    public async Task<Result<ContractResponse>> UpdateAsync(Guid id, ContractUpdateRequest entity, CancellationToken cancellationToken)
    {
        Contract? contract = await _repository.GetByIdAsync(id, cancellationToken);
        if (contract == null)
        {
            return Result<ContractResponse>.Failure("Error: Contract not found", StatusCodeEnum.NotFound);
        }
        contract.ContractStatus = entity.ContractStatus;

        Contract? response = await _repository.UpdateAsync(id, contract, cancellationToken);
        if (response == null)
        {
            return Result<ContractResponse>.Failure("Problem with settings", StatusCodeEnum.InternalServerError);
        }
        return Result<ContractResponse>.Success(response.ToContractResponse(), StatusCodeEnum.Ok);
    }


}
