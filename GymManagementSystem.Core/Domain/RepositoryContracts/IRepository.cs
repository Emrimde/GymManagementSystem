using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IRepository<Entity>
{
    Task<Entity> CreateAsync(Entity entity);
    Task<IEnumerable<Entity>> GetAllAsync(string? searchText = null);
    Task<Entity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Entity?> UpdateAsync(Guid id, Entity entity);
}