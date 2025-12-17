using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class VisitController : BaseController
{
    private readonly IVisitService _visitService;
    public VisitController(IVisitService visitService)
    {
        _visitService = visitService;
    }

    [HttpPost("register-visit/{clientId:guid}")]
    public async Task<ActionResult> RegisterVisit([FromRoute] Guid clientId)
        => HandleResult(await _visitService.RegisterVisitAsync(clientId));
}
