using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IDashboardService
{
    Task<Result<DashboardPlotsDataResponse>> GetDataForDashboardPlotsAsync();
    Task<Result<DashboardKpiResponse>> GetKPIAsync();
}
