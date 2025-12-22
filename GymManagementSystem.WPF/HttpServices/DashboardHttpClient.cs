using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.Core.Result;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagementSystem.WPF.HttpServices;

public class DashboardHttpClient : BaseHttpClientService
{
    public DashboardHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<DashboardKpiResponse>> GetAllDashboardKpiAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<DashboardKpiResponse>($"");

            return Result<DashboardKpiResponse>.Success(response ?? new DashboardKpiResponse());
        }


        catch (HttpRequestException ex)
        {
            return Result<DashboardKpiResponse>.Failure($"Error fetching dashboard stats: {ex.Message}");
        }
    }
}
