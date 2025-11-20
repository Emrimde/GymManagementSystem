namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IRepository<Entity>
{
    Task<Entity> CreateAsync(Entity entity, CancellationToken cancellationToken);
    Task<IEnumerable<Entity>> GetAllAsync(CancellationToken cancellationToken);
    Task<Entity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Entity?> UpdateAsync(Guid id, Entity entity, CancellationToken cancellationToken);
}