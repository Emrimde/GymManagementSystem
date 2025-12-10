using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Contract;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IContractRepository : IRepository<ContractResponse, Contract>
{
    Task<Contract?> GetActiveContractAsync(Guid clientId);
}
