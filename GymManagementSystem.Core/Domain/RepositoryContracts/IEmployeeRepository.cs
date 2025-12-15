using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Employee;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IEmployeeRepository
{
    Task<EmployeeInfoResponse> CreateEmployeeAsync(Employee employee);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync(string? searchText = null);
}
