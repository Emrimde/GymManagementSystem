using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.GymClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class GymClassController : BaseController
{
    private readonly IGymClassService _gymClassService;
    public GymClassController(IGymClassService gymClassService)
    {
        _gymClassService = gymClassService;
    }

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer,GroupInstructor")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GymClassResponse>>> GetAll([FromQuery] bool? isActive)
       => HandleListedResult(await _gymClassService.GetAllAsync(isActive));

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer,GroupInstructor,Client")]
    [HttpGet("select-gymclasses")]
    public async Task<ActionResult<IEnumerable<GymClassComboBoxResponse>>> GetGymClassesForSelect()
        => HandleListedResult(await _gymClassService.GetGymClassesForSelectAsync());

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet("get-gymclass-for-edit/{gymClassId:guid}")]
    public async Task<ActionResult<GymClassForEditResponse>> GetGymClassForEdit([FromRoute] Guid gymClassId)
        => HandleResult(await _gymClassService.GetGymClassForEditAsync(gymClassId));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPost]
    public async Task<ActionResult<GymClassInfoResponse>> Create([FromBody] GymClassAddRequest entity)
        => HandleResult(await _gymClassService.CreateAsync(entity));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPost("{gymCLassId:guid}")]
    public async Task<ActionResult<Unit>> GenerateScheduledClasses([FromRoute] Guid gymClassId)
        => HandleResult(await _gymClassService.GenerateNewScheduledClassesAsync(gymClassId));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPut]
    public async Task<ActionResult<Unit>> Update(GymClassUpdateRequest gymClassUpdateRequest)
        => HandleResult(await _gymClassService.UpdateAsync(gymClassUpdateRequest));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPut("restore/{gymClassId:guid}")]
    public async Task<ActionResult<Unit>> RestoreGymClass([FromRoute] Guid gymClassId)
        => HandleResult(await _gymClassService.RestoreGymClassAsync(gymClassId));

    [Authorize(Roles = "Owner,Manager")]
    [HttpDelete("{gymClassId:guid}")]
    public async Task<ActionResult<Unit>> DeleteGymClass([FromRoute] Guid gymClassId)
        => HandleResult(await _gymClassService.DeleteGymClassAsync(gymClassId));
}
