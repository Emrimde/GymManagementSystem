using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
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

    [HttpPut("trainer-timeoff/{id:guid}")]
    public async Task<ActionResult<TrainerInfoResponse>> UpdateTrainerOff(Guid id, TrainerTimeOffUpdateRequest entity)
        => HandleResult(await _trainerScheduleService.UpdateTrainerOff(id, entity));

    [HttpGet("schedule/{trainerId:guid}")]
    public async Task<ActionResult<TrainerScheduleResponse>> GetSchedule([FromRoute] Guid trainerId, [FromQuery] int days,  CancellationToken cancellationToken) => HandleResult(await _trainerScheduleService.GetTrainerScheduleAsync(trainerId, days, cancellationToken));

     [HttpPost("timeoff")]
    public async Task<ActionResult<TrainerInfoResponse>> CreateTrainerTimeOff([FromBody] TrainerTimeOffAddRequest entity) => HandleResult(await _trainerService.CreateTrainerTimeOffAsync(entity));

     [HttpGet("timeoffs")]
    public async Task<ActionResult<TrainerInfoResponse>> GetTrainerTimeOffs(CancellationToken cancellationToken) => HandleResult(await _trainerService.GetTrainerTimeOffs(cancellationToken));


    [HttpPost("personal-booking")]
    public async Task<ActionResult<PersonalBookingInfoResponse>> CreatePersonalBooking([FromBody] PersonalBookingAddRequest entity) => HandleResult(await _trainerScheduleService.CreatePersonalBookingAsync(entity));


    [HttpPost("trainercontract")]
    public async Task<ActionResult<TrainerContractInfoResponse>> CreateTrainerContract([FromBody] TrainerContractAddRequest entity) => HandleResult(await _trainerService.CreateTrainerContractAsync(entity));

    [HttpGet("trainercontracts")]
    public async Task<ActionResult<IEnumerable<TrainerContractInfoResponse>>> GetAllTrainerContracts(CancellationToken cancellationToken) => HandleListedResult(await _trainerService.GetAllTrainerContractsAsync(cancellationToken));

    [HttpGet("trainercontract/{id:guid}")]
    public async Task<ActionResult<TrainerContractDetailsResponse>> GetTrainerContracts([FromRoute] Guid id, [FromQuery] bool includeDetails, CancellationToken cancellationToken) => HandleResult(await _trainerService.GetTrainerContractAsync(id, includeDetails,cancellationToken));

    [HttpGet("instructors")]
    public async Task<ActionResult<IEnumerable<TrainerContractResponse>>> GetAllInstructors(CancellationToken cancellationToken) => HandleListedResult(await _trainerService.GetAllGetAllInstructorsAsync(cancellationToken));

}
