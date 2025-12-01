using GymManagementSystem.Core.DTO.EmploymentTemplate;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IEmploymentTemplateService
{
    Task<Result<EmploymentTemplateInfoResponse>> CreateEmploymentTemplateAsync(EmploymentTemplateAddRequest request);
    Task<Result<IEnumerable<EmploymentTemplateResponse>>> GetAllEmploymentTemplatesAsync(CancellationToken cancellationToken);
}
