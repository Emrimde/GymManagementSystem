using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.GymClass;
using GymManagementSystem.Core.WebDTO.ScheduledClassDto;
using GymManagementSystem.Core.WebDTO.Trainer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers.Client;

[Route("api/client/trainer")]
[ApiController]
[Authorize(Roles = "Trainer,GroupInstructor")]
public class TrainerController : BaseController
{
    private readonly ITrainerService _trainerService;

    public TrainerController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpGet("gym-classes")]
    public async Task<ActionResult<IEnumerable<GymClassDto>>> GetMyGymClasses() => HandleListedResult(await _trainerService.GetMyGymClassesAsync());


    [HttpGet("get-group-instructor-panel")]
    public async Task<ActionResult<GroupInstructorPanelResponse>> GetGroupInstructorPanel() => HandleResult(await _trainerService.GetGroupInstructorPanelAsync());


    [HttpGet("get-panel-info")]
    public async Task<ActionResult<IEnumerable<TrainerPanelInfoResponse>>> GetPersonalTrainerPanel(
        ) => HandleResult(await _trainerService.GetPersonalTrainerPanelAsync());

    [HttpPost("create-trainer-time-off")]
    public async Task<ActionResult> GetPersonalTrainerPanel([FromBody] TrainerTimeOffAddRequest request
        ) => HandleResult(await _trainerService.CreateTrainerTimeOffAsync(request));

}