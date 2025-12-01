using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.EmploymentTemplate;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IEmploymentTemplateRepository
{
    Task<EmploymentTemplateInfoResponse> CreateEmploymentTemplateAsync(EmploymentTemplate entity);
    Task<IEnumerable<EmploymentTemplate>> GetAllEmploymentTemplateAsync(CancellationToken cancellationToken);
}
