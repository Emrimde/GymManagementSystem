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
    public async Task<ActionResult<PageResult<ScheduledClassResponse>>> GetAll()
        => HandlePageResult(await _scheduledClassService.GetAllAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ScheduledClassDetailsResponse>> GetById([FromRoute] Guid id) => HandleResult(await _scheduledClassService.GetByIdAsync(id));

}
