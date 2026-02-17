using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

public class DashboardController : BaseController
{
    private readonly IDashboardService _dashboardService;
    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer,GroupInstructor")]
    [HttpGet]
    public async Task<ActionResult<DashboardKpiResponse>> GetKPI() => HandleResult(await _dashboardService.GetKPIAsync());

    [Authorize(Roles = "Owner,Manager,Receptionist,Trainer,GroupInstructor")]
    [HttpGet("get-visit-points-7days")]
    public async Task<ActionResult<DashboardPlotsDataResponse>> GetDataForDashboardPlotsAsync() => HandleResult(await _dashboardService.GetDataForDashboardPlotsAsync());
}
