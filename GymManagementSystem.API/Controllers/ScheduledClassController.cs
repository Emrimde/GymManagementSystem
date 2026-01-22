using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class ScheduledClassController : BaseController
{
    private readonly IScheduledClassService _scheduledClassService;
    public ScheduledClassController(IScheduledClassService scheduledClassService)
    {
        _scheduledClassService = scheduledClassService;
    }

    [HttpGet("by-gymclass/{gymClassId:guid}")]
    public async Task<ActionResult<IEnumerable<ScheduledClassResponse>>> GetAll([FromRoute] Guid gymClassId, [FromQuery] string? searchText = null)
        => HandleListedResult(await _scheduledClassService.GetAllAsync(gymClassId,searchText));

    [HttpGet("scheduledclasses/{gymClassId:guid}/{membershipId:guid}")]
    public async Task<ActionResult<IEnumerable<ScheduledClassComboBoxResponse>>> GetAllScheduledClassesByGymClassId([FromRoute] Guid gymClassId, [FromRoute] Guid membershipId)
        => HandleListedResult(await _scheduledClassService.GetAllScheduledClassesByGymClassId(gymClassId, membershipId));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ScheduledClassDetailsResponse>> GetById([FromRoute] Guid id) => HandleResult(await _scheduledClassService.GetByIdAsync(id));

}
