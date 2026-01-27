using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Employee;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;
public interface IEmployeeRepository
{
    void CreateEmployee(Employee employee);
    void DeleteEmployee(Employee employee);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync(string? searchText = null);
    Task<Employee?> GetEmployeeByIdAsync(Guid employeeId);
}
