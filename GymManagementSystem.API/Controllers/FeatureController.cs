using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;
public class FeatureController : BaseController
{
    private readonly IFeatureService _featureService;
    public FeatureController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllFeaturesForSelect() => HandleResult(await _featureService.GetFeaturesForSelect());    
}
