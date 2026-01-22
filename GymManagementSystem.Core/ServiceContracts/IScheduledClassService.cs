using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IScheduledClassService
{
    Task<Result<IEnumerable<ScheduledClassResponse>>> GetAllAsync(Guid gymClassId, string? searchText);
    Task<Result<IEnumerable<ScheduledClassComboBoxResponse>>> GetAllScheduledClassesByGymClassId(Guid gymClassId, Guid membershipId);
    Task<Result<ScheduledClassDetailsResponse>> GetByIdAsync(Guid id);
}
