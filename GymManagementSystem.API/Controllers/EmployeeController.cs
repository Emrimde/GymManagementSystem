
using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class EmployeeController : BaseController
{
    private readonly IEmployeeService _employeeService;
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeResponse>>> GetAllEmployees([FromQuery] string? searchText = null) => HandleListedResult(await _employeeService.GetAllEmployeesAsync(searchText));

    [HttpGet("{employeeId:guid}")]
    public async Task<ActionResult<EmployeeDetailsResponse>> GetEmployeeById([FromRoute] Guid employeeId) => HandleResult(await _employeeService.GetEmployeeByIdAsync(employeeId));

    [HttpPost]
    [Authorize (Roles = "Manager")]
    public async Task<ActionResult<EmployeeInfoResponse>> CreateEmployee([FromBody] EmployeeAddRequest request) => HandleResult(await _employeeService.CreateEmployeeAsync(request));
    
    [HttpPost("validate")]
    public ActionResult<bool> ValidateEmployee([FromBody] EmployeeAddRequest request) => HandleResult(_employeeService.ValidateEmployee(request));
    
}

