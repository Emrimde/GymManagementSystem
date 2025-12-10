using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IScheduledClassService
{
    Task<PageResult<ScheduledClassResponse>> GetAllAsync();
    Task<Result<ScheduledClassDetailsResponse>> GetByIdAsync(Guid id);
}
