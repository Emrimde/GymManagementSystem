using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IScheduledClassService
{
    Task<Result<IEnumerable<ScheduledClassResponse>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<ScheduledClassDetailsResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
