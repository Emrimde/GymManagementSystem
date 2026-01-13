using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.GeneralGymDetail;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers.Public;

[Route("api/public/generalGymDetail")]
public class GeneralGymDetailController : BaseController
{
    private readonly IGeneralGymDetailsService _generalGymDetailsService;
    public GeneralGymDetailController(IGeneralGymDetailsService generalGymDetailsService)
    {
        _generalGymDetailsService = generalGymDetailsService;
    }

    [HttpGet("get-profile")]
    public async Task<ActionResult<GeneralPublicProfileResponse?>> GetPublicGymProfile() => HandleResult(await _generalGymDetailsService.GetPublicGymProfileAsync());

    [HttpGet("get-about-us")]
    public async Task<ActionResult<GeneralPublicProfileResponse?>> GetAboutUs() => HandleResult(await _generalGymDetailsService.GetPublicAboutUsAsync());

}
