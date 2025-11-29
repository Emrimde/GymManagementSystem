using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IClientRepository : IRepository<Client>
{
    Task<IEnumerable<Client>> LookUpClientsAsync(string query, Guid? scheduledClassId = null);
}
