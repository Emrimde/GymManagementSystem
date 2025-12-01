using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepo;
    public EmployeeService(IEmployeeRepository employeeRepo)
    {
        _employeeRepo = employeeRepo;
    }

    public async Task<Result<EmployeeInfoResponse>> CreateEmployeeAsync(EmployeeAddRequest request)
    {
        Employee employee = request.ToEmployee();
        if(employee.EmployeeRole == EmployeeRole.Trainer)
        {
            employee.TrainerProfile = new TrainerProfile();
        }

        EmployeeInfoResponse response = await _employeeRepo.CreateEmployeeAsync(employee);
        return Result<EmployeeInfoResponse>.Success(response,StatusCodeEnum.Ok);

    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetAllEmployeesAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Employee> employees = await _employeeRepo.GetAllEmployeesAsync(cancellationToken);
        return Result<IEnumerable<EmployeeResponse>>.Success(employees.Select(item => item.ToEmployeeResponse()), StatusCodeEnum.Ok);
    }
}
