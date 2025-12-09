using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IPersonRepository
{
    Task<Person?> GetPersonByIdAsync(Guid personId);
}
