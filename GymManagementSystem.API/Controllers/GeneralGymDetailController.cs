using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class GeneralGymDetailController : BaseController
{
    private readonly IGeneralGymDetailsService _generalGymDetailsService;
    public GeneralGymDetailController(IGeneralGymDetailsService generalGymDetailsService)
    {
        _generalGymDetailsService = generalGymDetailsService;
    }
    [HttpGet]
    public async Task<ActionResult<GeneralGymResponse>> GetGeneralGymDetail() =>  HandleResult(await _generalGymDetailsService.GetSettingsByIdAsync());

    [HttpPut]
    public async Task<ActionResult<GeneralGymResponse>> UpdateGeneralSettings([FromBody] GeneralGymUpdateRequest request) => HandleResult(await _generalGymDetailsService.UpdateSettingsAsync(request));

}
