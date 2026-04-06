using GymManagementSystem.Core.DTO.GymClass;

namespace GymManagementSystem.Core.QueryContracts;
public interface IGymClassQueryService
{
    Task<IEnumerable<GymClassResponse>> GetGymClassesResponseAsync(bool? isActive);
}
