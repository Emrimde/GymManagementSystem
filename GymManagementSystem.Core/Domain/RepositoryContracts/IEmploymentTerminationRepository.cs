using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.EmploymentTermination;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IEmploymentTerminationRepository
{
    void AddEmploymentTermination(EmploymentTermination employmentTermination);
    Task<IEnumerable<EmploymentTerminationResponse>> GetEmploymentTerminationsAsync();
    Task<EmploymentTermination?> GetActiveEmploymentTerminationByPersonId(Guid personId);
}
