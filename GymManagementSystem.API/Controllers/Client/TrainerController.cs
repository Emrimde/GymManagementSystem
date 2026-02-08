using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.GymClass;
using GymManagementSystem.Core.WebDTO.PersonalBooking;
using GymManagementSystem.Core.WebDTO.ScheduledClassDto;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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


    [HttpGet("gym-classes/{gymClassId:guid}/scheduled-classes")]
    public async Task<ActionResult<IEnumerable<ScheduledClassDto>>> GetScheduledClasses(
        [FromRoute] Guid gymClassId) => HandleListedResult(await _trainerService.GetScheduledClassesForGymClassAsync(gymClassId));


    [HttpGet("get-personal-bookings")]
    public async Task<ActionResult<IEnumerable<PersonalBookingForTrainerResponse>>> GetPersonalBookings(
        [FromRoute] Guid gymClassId) => HandleListedResult(await _trainerService.GetTrainerPersonalBookingsAsync());

}