using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;

namespace GymManagementSystem.Core.Services;
public class DashboardService : IDashboardService
{
    private readonly IClientMembershipRepository _clientMembershipRepository;
    private readonly IVisitRepository _visitRepository;
    public DashboardService(IClientMembershipRepository clientMembershipRepository, IVisitRepository visitRepository)
    {
        _clientMembershipRepository = clientMembershipRepository;
        _visitRepository = visitRepository;
    }

    public async Task<Result<DashboardPlotsDataResponse>> GetDataForDashboardPlotsAsync()
    {
        DateTime startDate = DateTime.UtcNow.Date - TimeSpan.FromDays(6);
        DateTime endDate = DateTime.UtcNow.Date;

        IEnumerable<PointResponse> visits =
            await _visitRepository.GetAllVisitsOverTime(startDate, endDate);

        IEnumerable<PointResponse> memberships =
            await _clientMembershipRepository.GetAllClientMembershipsOverTime();

        DashboardPlotsDataResponse response = new()
        {
            VisitsPoints = FillMissingDays(visits, startDate, endDate),
            ClientMembershipsPoints = FillMissingDays(memberships, startDate, endDate)
        };

        return Result<DashboardPlotsDataResponse>.Success(response, StatusCodeEnum.Ok);
    }


    private static List<PointResponse> FillMissingDays(
    IEnumerable<PointResponse> source,
    DateTime startDate,
    DateTime endDate)
    {
        Dictionary<DateTime, int> dict =
            source.ToDictionary(item => item.Date.Date, item => item.TimeSeriesPoint);

        List<PointResponse> result = new();

        for (DateTime day = startDate.Date; day <= endDate.Date; day = day.AddDays(1))
        {
            result.Add(new PointResponse
            {
                Date = day,
                TimeSeriesPoint = dict.TryGetValue(day, out var value) ? value : 0
            });
        }

        return result;
    }


    public async Task<Result<DashboardKpiResponse>> GetKPIAsync()
    {
        int allActiveMemberships = await _clientMembershipRepository.GetActiveClientMembershipsCountAsync(null);
        int todayClientMembership = await _clientMembershipRepository.GetActiveClientMembershipsCountAsync(DateTime.UtcNow);
        int totalVisitsToday = await _visitRepository.GetTotalVisitsAsync(DateTime.UtcNow);

        DashboardKpiResponse response = new DashboardKpiResponse()
        {
            ActiveMemberships = allActiveMemberships,
            TodayClientMemberships = todayClientMembership,
            TotalVisitsToday = totalVisitsToday
        };

        return Result<DashboardKpiResponse>.Success(response, StatusCodeEnum.Ok);
    }
}
