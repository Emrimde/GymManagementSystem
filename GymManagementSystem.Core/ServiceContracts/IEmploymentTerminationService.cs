using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.Core.Result;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;
public interface IEmploymentTerminationService
{
    Task<Result<Unit>> CreateEmploymentTerminationAsync(EmploymentTerminationAddRequest request);
    Task<Result<EmploymentTerminationGenerateResponse>> GetEmployeeEmploymentTerminationsAsync(Guid personId);
    Task<Result<EmploymentTerminationGenerateResponse>> GetEmploymentTerminationDetailsAsync(Guid personId);
    Task<Result<IEnumerable<EmploymentTerminationResponse>>> GetEmploymentTerminationsAsync();
}