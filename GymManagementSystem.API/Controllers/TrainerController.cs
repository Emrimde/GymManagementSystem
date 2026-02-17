using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer")]
    [HttpPut("trainer-timeoff/{id:guid}")]
    public async Task<ActionResult<TrainerInfoResponse>> UpdateTrainerOff(Guid id, TrainerTimeOffUpdateRequest entity)
        => HandleResult(await _trainerScheduleService.UpdateTrainerOff(id, entity));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("schedule/{trainerId:guid}")]
    public async Task<ActionResult<TrainerScheduleResponse>> GetSchedule([FromRoute] Guid trainerId, [FromQuery] int days,  CancellationToken cancellationToken) => HandleResult(await _trainerScheduleService.GetTrainerScheduleAsync(trainerId, days, cancellationToken));

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer")]
    [HttpPost("timeoff")]
    public async Task<ActionResult> CreateTrainerTimeOff([FromBody] TrainerTimeOffAddRequest entity) => HandleResult(await _trainerService.CreateTrainerTimeOffAsync(entity));

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer")]
    [HttpGet("timeoffs")]
    public async Task<ActionResult<TrainerInfoResponse>> GetTrainerTimeOffs(CancellationToken cancellationToken) => HandleResult(await _trainerService.GetTrainerTimeOffs(cancellationToken));

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer")]
    [HttpGet("get-timeoff-reason/{trainerTimeOffId:guid}")]
    public async Task<ActionResult<TrainerTimeOffReasonResponse>> GetTimeOffReason([FromRoute] Guid trainerTimeOffId) => HandleResult(await _trainerService.GetTimeOffReasonAsync(trainerTimeOffId));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPost("trainercontract")]
    public async Task<ActionResult<TrainerContractCreatedResponse>> CreateTrainerContract([FromBody] TrainerContractAddRequest entity) => HandleResult(await _trainerService.CreateTrainerContractAsync(entity));

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer")]
    [HttpDelete("trainer-time-off-delete/{trainerTimeOffId:guid}")]
    public async Task<ActionResult> DeleteTrainerTimeOff([FromRoute] Guid trainerTimeOffId) => HandleResult(await _trainerService.DeleteTrainerTimeOffAsync(trainerTimeOffId));

    [Authorize(Roles = "Owner,Manager,Receptionist")]
    [HttpGet("trainercontract/{id:guid}")]
    public async Task<ActionResult<TrainerContractDetailsResponse>> GetTrainerContract([FromRoute] Guid id, [FromQuery] bool includeDetails, CancellationToken cancellationToken) => HandleResult(await _trainerService.GetTrainerContractAsync(id, includeDetails,cancellationToken));

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet("instructors")]
    public async Task<ActionResult<IEnumerable<TrainerContractResponse>>> GetAllInstructors(CancellationToken cancellationToken) => HandleListedResult(await _trainerService.GetAllInstructorsAsync(cancellationToken));

    [Authorize(Roles = "Owner,Manager,Receptionist,Client")]
    [HttpGet("personal-trainers")]
    public async Task<ActionResult<IEnumerable<TrainerInfoResponse>>> GetAllPersonalTrainers() => HandleListedResult(await _trainerService.GetAllPersonalTrainersAsync());

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet("trainer-rates/{id:guid}")]
    public async Task<ActionResult<IEnumerable<TrainerRateResponse>>> GetTrainerRates([FromRoute] Guid id, [FromQuery] bool? showActive) => HandleListedResult(await _trainerService.GetAllTrainerRatesAsync(id, showActive));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPost("trainer-rate")]
    public async Task<ActionResult<TrainerRateInfoResponse>> CreateTrainerRate([FromBody] TrainerRateAddRequest request) => HandleResult(await _trainerService.CreateTrainerRateAsync(request));

    [Authorize(Roles = "Owner,Manager,Client")]
    [HttpGet("trainer-rates-select/{id:guid}")]
    public async Task<ActionResult<IEnumerable<TrainerRateSelectResponse>>> GetTrainerRatesSelect([FromRoute] Guid id) => HandleListedResult(await _trainerService.GetTrainerRatesSelect(id));

}
