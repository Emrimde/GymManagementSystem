using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;


public class TrainerController : BaseController
{
    private readonly ITrainerService _trainerService;
    public TrainerController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TrainerResponse>>> GetAll(CancellationToken cancellationToken)
       => HandleListedResult(await _trainerService.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TrainerDetailsResponse>> GetById(Guid id, [FromQuery] bool isActiveOnly, CancellationToken cancellationToken)
        => HandleResult(await _trainerService.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    public async Task<ActionResult<TrainerInfoResponse>> Create([FromBody] TrainerAddRequest entity, CancellationToken cancellationToken)
        => HandleResult(await _trainerService.CreateAsync(entity, cancellationToken));

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<TrainerInfoResponse>> Update(Guid id, TrainerUpdateRequest entity, CancellationToken cancellationToken)
        => HandleResult(await _trainerService.UpdateAsync(id, entity, cancellationToken));
}
