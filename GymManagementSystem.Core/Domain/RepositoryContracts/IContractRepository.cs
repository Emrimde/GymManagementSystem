using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IContractRepository : IRepository<Contract>
{
    Task<Contract?> GetActiveContractAsync(Guid clientId);
}
