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

        EmployeeInfoResponse response = await _employeeRepo.CreateEmployeeAsync(employee);
        return Result<EmployeeInfoResponse>.Success(response, StatusCodeEnum.Ok);

    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetAllEmployeesAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Employee> employees = await _employeeRepo.GetAllEmployeesAsync(cancellationToken);
        return Result<IEnumerable<EmployeeResponse>>.Success(employees.Select(item => item.ToEmployeeResponse()), StatusCodeEnum.Ok);
    }

    public Result<bool> ValidateEmployee(EmployeeAddRequest request)
    {
        if (request.ContractTypeEnum == ContractTypeEnum.Probation)
        {
            if (request.ValidFrom > request.ValidTo)
            {
                return Result<bool>.Failure("Valid from is newer date than valid to field", StatusCodeEnum.BadRequest);
            }
        }
        return Result<bool>.Success(true);
    }
}
