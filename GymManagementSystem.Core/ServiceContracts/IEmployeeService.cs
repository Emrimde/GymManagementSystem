
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IEmployeeService
{
    Task<Result<EmployeeInfoResponse>> CreateEmployeeAsync(EmployeeAddRequest request);
    Task<Result<IEnumerable<EmployeeResponse>>> GetAllEmployeesAsync(CancellationToken cancellationToken);
}
