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
    public async Task<ActionResult<IEnumerable<GymClassResponse>>> GetAll(CancellationToken cancellationToken)
       => HandleListedResult(await _gymClassService.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GymClassDetailsResponse>> GetById(Guid id, [FromQuery] bool isActiveOnly, CancellationToken cancellationToken)
        => HandleResult(await _gymClassService.GetByIdAsync(id, cancellationToken));

    [HttpGet("select-gymclasses")]
    public async Task<ActionResult<IEnumerable<GymClassComboBoxResponse>>> GetGymClassesForSelect()
        => HandleListedResult(await _gymClassService.GetGymClassesForSelectAsync());

    [HttpPost]
    public async Task<ActionResult<GymClassInfoResponse>> Create([FromBody] GymClassAddRequest entity)
        => HandleResult(await _gymClassService.CreateAsync(entity));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GymClassInfoResponse>> Update(Guid id, GymClassUpdateRequest entity)
        => HandleResult(await _gymClassService.UpdateAsync(id, entity));
}
