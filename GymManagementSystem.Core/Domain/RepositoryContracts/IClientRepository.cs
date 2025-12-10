using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Client;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IClientRepository : IRepository<ClientResponse,Client>
{
    Task<IEnumerable<Client>> LookUpClientsAsync(string query, Guid? scheduledClassId = null);
}
