using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.API.Controllers;
public interface IEmploymentTerminationService
{
    Task<Result<EmploymentTerminationInfoResponse>> CreateEmploymentTerminationAsync(EmploymentTerminationAddRequest request);
    Task<Result<EmploymentTerminationGenerateResponse>> GetEmploymentTerminationDetailsAsync(Guid personId);
    Task<Result<IEnumerable<EmploymentTerminationResponse>>> GetEmploymentTerminationsAsync();
}