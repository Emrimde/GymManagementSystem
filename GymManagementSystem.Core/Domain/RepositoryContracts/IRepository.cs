namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IRepository<Response,Entity>
{
    void CreateAsync(Entity entity);
    Task<Entity?> GetByIdAsync(Guid id);
    Task<Entity?> UpdateAsync(Guid id, Entity entity);
}