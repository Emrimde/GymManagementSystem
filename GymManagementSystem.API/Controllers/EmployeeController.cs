
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

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet("{employeeId:guid}")]
    public async Task<ActionResult<EmployeeDetailsResponse>> GetEmployeeById([FromRoute] Guid employeeId) => HandleResult(await _employeeService.GetEmployeeByIdAsync(employeeId));

    [Authorize(Roles = "Manager,Owner")]
    [HttpPost]
    public async Task<ActionResult<EmployeeInfoResponse>> CreateEmployee([FromBody] EmployeeAddRequest request) => HandleResult(await _employeeService.CreateEmployeeAsync(request));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPost("get-employee-contract")]
    public async Task<ActionResult<EmploymentContractPdfDto>> BuildEmployeeContract([FromBody] EmployeeContractRequest request) => HandleResult( await _employeeService.BuildEmployeeContractAsync(request));
    
}

