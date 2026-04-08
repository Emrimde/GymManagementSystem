using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.WebDTO.GymClass;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IGymClassRepository : IRepository<GymClassResponse, GymClass>
{
    Task<IEnumerable<GymClass>> GetAllAsync(bool? isActive);
    Task<List<GymClassDto>> GetByTrainerPersonIdAsync(Guid personId);
    Task<bool> ExistsOverlapAsync(GymClass gymClass);
}
