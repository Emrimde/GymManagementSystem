using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.WebDTO;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IClientRepository : IRepository<ClientResponse,Client>
{
    Task<ClientDetailsWebResponse?> GetClientProfileInfoAsync(Guid clientId);
    Task UpdateClientAsync(Client client);
    Task<ClientInfoResponse?> GetClientFullNameByIdAsync(Guid id);
    Task<IEnumerable<Client>> LookUpClientsAsync(string query, Guid? scheduledClassId = null);
}
