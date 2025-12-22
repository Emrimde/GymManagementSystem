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
