using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class TerminationController : BaseController
{
    private readonly ITerminationService _terminationService;

    public TerminationController(ITerminationService terminationService)
    {
        _terminationService = terminationService;
    }

    [Authorize(Roles = "Owner,Manager")]
    [HttpPost]
    public async Task<ActionResult<TerminationResponse>> PostTermination([FromBody] TerminationAddRequest request)
    => HandleResult(await _terminationService.CreateAsync(request));
}
