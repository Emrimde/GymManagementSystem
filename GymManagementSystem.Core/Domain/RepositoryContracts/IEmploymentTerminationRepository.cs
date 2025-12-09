using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.EmploymentTermination;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IEmploymentTerminationRepository
{
    Task<EmploymentTerminationInfoResponse> AddEmploymentTermination(EmploymentTermination employmentTermination);
    Task<IEnumerable<EmploymentTerminationResponse>> GetEmploymentTerminationsAsync();
}
