using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Feature;
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
    public async Task<ActionResult> GetAllFeatures() => HandleResult(await _featureService.GetAllFeaturesAsync());    
    [HttpPost]
    public async Task<ActionResult> CreateFeature([FromBody] FeatureAddRequest featureAddRequest) => HandleResult(await _featureService.CreateFeatureAsync(featureAddRequest));    
}
