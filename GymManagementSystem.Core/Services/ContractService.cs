using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;

public class ContractService : IContractService
{
    private readonly IContractRepository _repository;
    public ContractService(IContractRepository repository)
    {
        _repository = repository;
    }

    public Task<Result<ContractResponse>> CreateAsync(ContractAddRequest entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ContractDetailsResponse>> GetByIdAsync(Guid id)
    {
        Contract? contract = await _repository.GetByIdAsync(id);
        if (contract == null)
        {
            return Result<ContractDetailsResponse>.Failure("Contract not found", StatusCodeEnum.NotFound);
        }
        return Result<ContractDetailsResponse>.Success(contract.ToContractDetails(), StatusCodeEnum.Ok);
    }

    public async Task<Result<ContractResponse>> UpdateAsync(Guid id, ContractUpdateRequest entity)
    {
        Contract? response = await _repository.UpdateAsync(id, entity.ToContract());
        if (response == null)
        {
            return Result<ContractResponse>.Failure("Problem with settings", StatusCodeEnum.InternalServerError);
        }
        return Result<ContractResponse>.Success(response.ToContractResponse(), StatusCodeEnum.Ok);
    }
}
