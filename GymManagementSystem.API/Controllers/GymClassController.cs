using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.DTO.Trainer;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class GymClassController : BaseController
{
    private readonly IGymClassService _gymClassService;
    public GymClassController(IGymClassService gymClassService)
    {
        _gymClassService = gymClassService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GymClassResponse>>> GetAll([FromQuery] bool? isActive)
       => HandleListedResult(await _gymClassService.GetAllAsync(isActive));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GymClassDetailsResponse>> GetById(Guid id, [FromQuery] bool isActiveOnly, CancellationToken cancellationToken)
        => HandleResult(await _gymClassService.GetByIdAsync(id, cancellationToken));

    [HttpGet("select-gymclasses")]
    public async Task<ActionResult<IEnumerable<GymClassComboBoxResponse>>> GetGymClassesForSelect()
        => HandleListedResult(await _gymClassService.GetGymClassesForSelectAsync());

    [HttpGet("get-gymclass-for-edit/{gymClassId:guid}")]
    public async Task<ActionResult<GymClassForEditResponse>> GetGymClassForEdit([FromRoute] Guid gymClassId)
        => HandleResult(await _gymClassService.GetGymClassForEditAsync(gymClassId));

    [HttpPost]
    public async Task<ActionResult<GymClassInfoResponse>> Create([FromBody] GymClassAddRequest entity)
        => HandleResult(await _gymClassService.CreateAsync(entity));

    [HttpPost("{gymCLassId:guid}")]
    public async Task<ActionResult<Unit>> GenerateScheduledClasses([FromRoute] Guid gymClassId)
        => HandleResult(await _gymClassService.GenerateNewScheduledClassesAsync(gymClassId));

    [HttpPut]
    public async Task<ActionResult<Unit>> Update(GymClassUpdateRequest gymClassUpdateRequest)
        => HandleResult(await _gymClassService.UpdateAsync(gymClassUpdateRequest));

    [HttpPut("restore/{gymClassId:guid}")]
    public async Task<ActionResult<Unit>> RestoreGymClass([FromRoute] Guid gymClassId)
        => HandleResult(await _gymClassService.RestoreGymClassAsync(gymClassId));


    [HttpDelete("{gymClassId:guid}")]
    public async Task<ActionResult<Unit>> DeleteGymClass([FromRoute] Guid gymClassId)
        => HandleResult(await _gymClassService.DeleteGymClassAsync(gymClassId));
}
