using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IRepository<Response,Entity>
{
    Task<Entity> CreateAsync(Entity entity);
    //Task<PageResult<Response>> GetAllAsync(string? searchText = null);
    Task<PageResult<Response>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null);
    Task<Entity?> GetByIdAsync(Guid id);
    Task<Entity?> UpdateAsync(Guid id, Entity entity);
}