using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Resulttttt;
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

    [HttpPut("trainer-timeoff/{id:guid}")]
    public async Task<ActionResult<TrainerInfoResponse>> UpdateTrainerOff(Guid id, TrainerTimeOffUpdateRequest entity)
        => HandleResult(await _trainerScheduleService.UpdateTrainerOff(id, entity));

    [HttpGet("schedule/{trainerId:guid}")]
    public async Task<ActionResult<TrainerScheduleResponse>> GetSchedule([FromRoute] Guid trainerId, [FromQuery] int days,  CancellationToken cancellationToken) => HandleResult(await _trainerScheduleService.GetTrainerScheduleAsync(trainerId, days, cancellationToken));

     [HttpPost("timeoff")]
    public async Task<ActionResult> CreateTrainerTimeOff([FromBody] TrainerTimeOffAddRequest entity) => HandleResult(await _trainerService.CreateTrainerTimeOffAsync(entity));

     [HttpGet("timeoffs")]
    public async Task<ActionResult<TrainerInfoResponse>> GetTrainerTimeOffs(CancellationToken cancellationToken) => HandleResult(await _trainerService.GetTrainerTimeOffs(cancellationToken));

     [HttpGet("get-timeoff-reason/{trainerTimeOffId:guid}")]
    public async Task<ActionResult<TrainerTimeOffReasonResponse>> GetTimeOffReason([FromRoute] Guid trainerTimeOffId) => HandleResult(await _trainerService.GetTimeOffReasonAsync(trainerTimeOffId));

    [HttpPost("trainercontract")]
    public async Task<ActionResult<TrainerContractInfoResponse>> CreateTrainerContract([FromBody] TrainerContractAddRequest entity) => HandleResult(await _trainerService.CreateTrainerContractAsync(entity));

    [HttpDelete("trainer-time-off-delete/{trainerTimeOffId:guid}")]
    public async Task<ActionResult> DeleteTrainerTimeOff([FromRoute] Guid trainerTimeOffId) => HandleResult(await _trainerService.DeleteTrainerTimeOffAsync(trainerTimeOffId));

    [HttpGet("trainercontracts")]
    public async Task<ActionResult<PageResult<TrainerContractInfoResponse>>> GetAllTrainerContracts( [FromQuery] int page = 1, [FromQuery] string? searchText = null, [FromQuery] int pageSize = 50) => HandlePageResult(await _trainerService.GetAllTrainerContractsAsync(page,searchText,pageSize));

    [HttpGet("trainercontract/{id:guid}")]
    public async Task<ActionResult<TrainerContractDetailsResponse>> GetTrainerContracts([FromRoute] Guid id, [FromQuery] bool includeDetails, CancellationToken cancellationToken) => HandleResult(await _trainerService.GetTrainerContractAsync(id, includeDetails,cancellationToken));

    [HttpGet("instructors")]
    public async Task<ActionResult<IEnumerable<TrainerContractResponse>>> GetAllInstructors(CancellationToken cancellationToken) => HandleListedResult(await _trainerService.GetAllInstructorsAsync(cancellationToken));

    [HttpGet("personal-trainers")]
    public async Task<ActionResult<IEnumerable<TrainerInfoResponse>>> GetAllPersonalTrainers() => HandleListedResult(await _trainerService.GetAllPersonalTrainersAsync());

    [HttpGet("trainer-rates/{id:guid}")]
    public async Task<ActionResult<IEnumerable<TrainerRateResponse>>> GetTrainerRates([FromRoute] Guid id) => HandleListedResult(await _trainerService.GetAllTrainerRatesAsync(id));



    [HttpPost("trainer-rate")]
    public async Task<ActionResult<TrainerRateInfoResponse>> CreateTrainerRate([FromBody] TrainerRateAddRequest request) => HandleResult(await _trainerService.CreateTrainerRateAsync(request));

    [HttpGet("trainer-rates-select/{id:guid}")]
    public async Task<ActionResult<IEnumerable<TrainerRateSelectResponse>>> GetTrainerRatesSelect([FromRoute] Guid id) => HandleListedResult(await _trainerService.GetTrainerRatesSelect(id));

}
