using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts
{
    public interface IContractService
    {
        Task<Result<ContractResponse>> CreateAsync(ContractAddRequest entity);
        Task<Result<ContractDetailsResponse>> GetByIdAsync(Guid id);
        Task<Result<ContractResponse>> UpdateAsync(Guid id, ContractUpdateRequest entity);
    }
}