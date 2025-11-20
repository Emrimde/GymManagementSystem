using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts
{
    public interface IContractService
    {
        Task<Result<ContractResponse>> CreateAsync(ContractAddRequest entity, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ContractResponse>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<ContractDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<ContractResponse>> UpdateAsync(Guid id, ContractUpdateRequest entity, CancellationToken cancellationToken);
    }
}