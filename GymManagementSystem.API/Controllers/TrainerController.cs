using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;


public class TrainerController : BaseController
{
    private readonly ITrainerService _trainerService;
    private readonly ITrainerScheduleService _trainerScheduleService;

    public TrainerController(ITrainerService trainerService,ITrainerScheduleService trainerScheduleService)
    {
        _trainerService = trainerService;
        _trainerScheduleService = trainerScheduleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainerResponse>>> GetAll(CancellationToken cancellationToken)
       => HandleListedResult(await _trainerService.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TrainerDetailsResponse>> GetById(Guid id, [FromQuery] bool isActiveOnly, CancellationToken cancellationToken)
        => HandleResult(await _trainerService.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<TrainerInfoResponse>> Create([FromBody] TrainerAddRequest entity, CancellationToken cancellationToken)
        => HandleResult(await _trainerService.CreateAsync(entity));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TrainerInfoResponse>> Update(Guid id, TrainerUpdateRequest entity)
        => HandleResult(await _trainerService.UpdateAsync(id, entity));

    //[HttpGet("{id:guid}/calendar")]
    //public async Task<ActionResult<TrainerScheduleResponse>> GetSchedule([FromRoute] Guid id,CancellationToken cancellationToken, [FromQuery] int days = 30) => HandleResult(await _trainerAvailabilityService.GetScheduleAsync(id, days, cancellationToken));


    [HttpGet("schedule/{trainerId:guid}")]
    public async Task<ActionResult<TrainerScheduleResponse>> GetSchedule([FromRoute] Guid trainerId, [FromQuery] int days,  CancellationToken cancellationToken) => HandleResult(await _trainerScheduleService.GetTrainerScheduleAsync(trainerId, days, cancellationToken));

     [HttpPost("timeoff")]
    public async Task<ActionResult<TrainerInfoResponse>> Create([FromBody] TrainerTimeOffAddRequest entity) => HandleResult(await _trainerService.CreateTrainerTimeOffAsync(entity));

     [HttpGet("timeoffs")]
    public async Task<ActionResult<TrainerInfoResponse>> GetTrainerTimeOffs(CancellationToken cancellationToken) => HandleResult(await _trainerService.GetTrainerTimeOffs(cancellationToken));
}
