using GymManagementSystem.API.Controllers.Base;
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ScheduledClassResponse>>> GetAll([FromQuery] string? searchText = null)
        => HandleListedResult(await _scheduledClassService.GetAllAsync(searchText));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ScheduledClassDetailsResponse>> GetById([FromRoute] Guid id) => HandleResult(await _scheduledClassService.GetByIdAsync(id));

}
