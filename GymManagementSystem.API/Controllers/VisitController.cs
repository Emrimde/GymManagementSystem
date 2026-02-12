using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO;
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
    public async Task<ActionResult> RegisterVisit([FromRoute] Guid clientId, [FromQuery] string? guestName)
        => HandleResult(await _visitService.RegisterVisitAsync(clientId, guestName));

    [HttpGet("{clientId:guid}")]
    public async Task<ActionResult<IEnumerable<VisitResponse>>> GetAllClientVisits([FromRoute] Guid clientId)
        => HandleListedResult(await _visitService.GetAllClientVisitsAsync(clientId));

    [HttpDelete("{visitId:guid}")]
    public async Task<ActionResult> DeleteVisit([FromRoute] Guid visitId)
        => HandleResult(await _visitService.DeleteVisitAsync(visitId));
}
