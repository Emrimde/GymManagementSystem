using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.WPF.Result;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class DashboardHttpClient : BaseHttpClientService
{
    public DashboardHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<DashboardKpiResponse>> GetAllDashboardKpiAsync()
    {
        return GetAsync<DashboardKpiResponse>("");
    }

    public Task<Result<DashboardPlotsDataResponse>> GetAllPointsAsync()
    {
        return GetAsync<DashboardPlotsDataResponse>(
            "get-visit-points-7days"
        );
    }
}
