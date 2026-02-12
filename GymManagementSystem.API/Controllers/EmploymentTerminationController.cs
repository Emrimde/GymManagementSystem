using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.EmploymentTermination;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;
public class EmploymentTerminationController : BaseController
{
    private readonly IEmploymentTerminationService _employmentTerminationService;
    public EmploymentTerminationController(IEmploymentTerminationService employmentTerminationService)
    {
        _employmentTerminationService = employmentTerminationService;
    }

    [HttpGet("{personId:guid}")]
    public async Task<ActionResult<EmploymentTerminationGenerateResponse>> GetEmploymentTerminationDetails([FromRoute] Guid personId) => HandleResult(await _employmentTerminationService.GetEmploymentTerminationDetailsAsync(personId));

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmploymentTerminationResponse>>> GetEmploymentTerminations() => HandleResult(await _employmentTerminationService.GetEmploymentTerminationsAsync());

    [HttpPost]
    public async Task<ActionResult> CreateEmploymentTerminationAsync([FromBody] EmploymentTerminationAddRequest request) => HandleResult(await _employmentTerminationService.CreateEmploymentTerminationAsync(request));
}
