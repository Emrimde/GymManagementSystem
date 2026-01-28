using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.ScheduledClass;
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
    public async Task<ActionResult<IEnumerable<ScheduledClassResponse>>> GetAll([FromRoute] Guid gymClassId)
        => HandleListedResult(await _scheduledClassService.GetAllAsync(gymClassId));

    [HttpGet("scheduledclasses/{gymClassId:guid}/{membershipId:guid}/{clientId:guid}")]
    public async Task<ActionResult<IEnumerable<ScheduledClassComboBoxResponse>>> GetAllScheduledClassesByGymClassId([FromRoute] Guid gymClassId, [FromRoute] Guid membershipId, [FromRoute] Guid clientId)
        => HandleListedResult(await _scheduledClassService.GetAllScheduledClassesByGymClassId(gymClassId, membershipId, clientId));

    [HttpDelete("cancel-schedule-class/{scheduleClassId:guid}")]
    public async Task<ActionResult<Unit>> CancelScheduleClass([FromRoute] Guid scheduleClassId)
        => HandleResult(await _scheduledClassService.CancelScheduleClassAsync(scheduleClassId));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ScheduledClassDetailsResponse>> GetById([FromRoute] Guid id) => HandleResult(await _scheduledClassService.GetByIdAsync(id));

}
