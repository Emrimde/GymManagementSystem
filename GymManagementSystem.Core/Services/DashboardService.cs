using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using System.Linq;

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

    public async Task<Result<IEnumerable<PointResponse>>> GetAllVisitsForLastDaysAsync()
    {
        DateTime startDate = DateTime.UtcNow.Date - TimeSpan.FromDays(7);
        DateTime endDate = DateTime.UtcNow.Date;
        IEnumerable<PointResponse> points = await _visitRepository.GetAllVisitsFromLast7Days(startDate, endDate);
        List<PointResponse> allDays = new List<PointResponse>();
        Dictionary<DateTime, int> pointsDict = points.ToDictionary(item => item.Date, item => item.VisitsNumber);

        for (DateTime day = startDate; day <= endDate; day = day.AddDays(1))
        {
            PointResponse point = new PointResponse()
            {
                Date = day
            };

            if (pointsDict.TryGetValue(day.Date, out var numberOfVisits))
            {
                point.VisitsNumber = numberOfVisits;
                allDays.Add(point);
                continue;
            }
            point.VisitsNumber = 0;
            allDays.Add(point);
        }
        
        return Result<IEnumerable<PointResponse>>.Success(allDays, StatusCodeEnum.Ok);
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
