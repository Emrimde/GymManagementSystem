using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.API.Controllers;
public interface IGymClassService
{
    Task<Result<GymClassInfoResponse>> CreateAsync(GymClassAddRequest entity);
    Task<Result<IEnumerable<GymClassResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<GymClassDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<GymClassInfoResponse>> UpdateAsync(Guid id, GymClassUpdateRequest entity);
}