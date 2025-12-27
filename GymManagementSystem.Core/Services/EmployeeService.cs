using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Core.Services;
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepo;
    private readonly IPersonRepository _personRepo;
    private readonly UserManager<User> _userManager;
    public EmployeeService(IEmployeeRepository employeeRepo, UserManager<User> userManager, IPersonRepository personRepo)
    {
        _employeeRepo = employeeRepo;
        _userManager = userManager;
        _personRepo = personRepo;
    }

    public async Task<Result<EmployeeInfoResponse>> CreateEmployeeAsync(EmployeeAddRequest request)
    {
        Employee employee = request.ToEmployee();
        Person? person = await _personRepo.GetPersonByIdAsync(employee.PersonId);
        if (person == null) {
            return Result<EmployeeInfoResponse>.Failure("Unexpected error during searching person data", StatusCodeEnum.InternalServerError);
        }
        User user = new User()
        {
            UserName = $"{person.FirstName + person.LastName}",
        };
        var createResult = await _userManager.CreateAsync(user, "employee");
        if (!createResult.Succeeded)
        {
            return Result<EmployeeInfoResponse>.Failure($"{createResult.Errors}", StatusCodeEnum.InternalServerError);
        }
        await _userManager.AddToRoleAsync(user, "Receptionist");

        EmployeeInfoResponse response = await _employeeRepo.CreateEmployeeAsync(employee);
        return Result<EmployeeInfoResponse>.Success(response, StatusCodeEnum.Ok);

    }

    public async Task<Result<IEnumerable<EmployeeResponse>>> GetAllEmployeesAsync(string? searchText = null)
    {
        IEnumerable<Employee> employees = await _employeeRepo.GetAllEmployeesAsync(searchText);
        return Result<IEnumerable<EmployeeResponse>>.Success(employees.Select(item => item.ToEmployeeResponse()), StatusCodeEnum.Ok);
    }

    public async Task<Result<EmployeeDetailsResponse>> GetEmployeeByIdAsync(Guid employeeId)
    {
        Employee? employee = await _employeeRepo.GetEmployeeByIdAsync(employeeId);
       return Result<EmployeeDetailsResponse>.Success(employee.ToEmployeeDetailsResponse(), StatusCodeEnum.Ok);
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
