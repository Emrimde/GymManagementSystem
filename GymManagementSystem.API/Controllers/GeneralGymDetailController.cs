using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class GeneralGymDetailController : BaseController
{
    private readonly IGeneralGymDetailsService _generalGymDetailsService;
    public GeneralGymDetailController(IGeneralGymDetailsService generalGymDetailsService)
    {
        _generalGymDetailsService = generalGymDetailsService;
    }

    [Authorize(Roles = "Owner,Manager")]
    [HttpGet]
    public async Task<ActionResult<GeneralGymResponse>> GetGeneralGymDetail() =>  HandleResult(await _generalGymDetailsService.GetSettingsByIdAsync());

    [Authorize(Roles = "Owner,Manager")]
    [HttpPut]
    public async Task<ActionResult<GeneralGymResponse>> UpdateGeneralSettings([FromBody] GeneralGymUpdateRequest request) => HandleResult(await _generalGymDetailsService.UpdateSettingsAsync(request));

    [Authorize(Roles = "Owner,Manager")]
    [HttpPost("logo")]
    public async Task<ActionResult<string>> UploadLogo(IFormFile file) => HandleResult(await _generalGymDetailsService.UploadLogoAsync(file));
}
