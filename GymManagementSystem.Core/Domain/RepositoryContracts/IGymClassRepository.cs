using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GymClass;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IGymClassRepository : IRepository<GymClassResponse, GymClass>
{
    Task<IEnumerable<GymClass>> GetAllAsync();
}
