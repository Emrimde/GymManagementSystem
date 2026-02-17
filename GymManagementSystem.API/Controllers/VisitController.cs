using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class VisitController : BaseController
{
    private readonly IVisitService _visitService;
    public VisitController(IVisitService visitService)
    {
        _visitService = visitService;
    }

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpPost("register-visit/{clientId:guid}")]
    public async Task<ActionResult> RegisterVisit([FromRoute] Guid clientId, [FromQuery] string? guestName)
        => HandleResult(await _visitService.RegisterVisitAsync(clientId, guestName));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("{clientId:guid}")]
    public async Task<ActionResult<IEnumerable<VisitResponse>>> GetAllClientVisits([FromRoute] Guid clientId)
        => HandleListedResult(await _visitService.GetAllClientVisitsAsync(clientId));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpDelete("{visitId:guid}")]
    public async Task<ActionResult> DeleteVisit([FromRoute] Guid visitId)
        => HandleResult(await _visitService.DeleteVisitAsync(visitId));
}
