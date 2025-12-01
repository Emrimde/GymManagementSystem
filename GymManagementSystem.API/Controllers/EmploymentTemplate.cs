using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.EmploymentTemplate;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;
public class EmploymentTemplate : BaseController
{
    private readonly IEmploymentTemplateService _employmentTemplateService;
    public EmploymentTemplate(IEmploymentTemplateService employmentTemplateService)
    {
        _employmentTemplateService = employmentTemplateService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmploymentTemplateResponse>>> GetAllEmployees(CancellationToken cancellationToken) => HandleListedResult(await _employmentTemplateService.GetAllEmploymentTemplatesAsync(cancellationToken));

    [HttpPost]
    public async Task<ActionResult<EmploymentTemplateInfoResponse>> CreateEmploymentTemplate([FromBody] EmploymentTemplateAddRequest request) => HandleResult(await _employmentTemplateService.CreateEmploymentTemplateAsync(request));
}
