namespace GymManagementSystem.Core.DTO.Dashboard;
public class DashboardPlotsDataResponse
{
    public IEnumerable<PointResponse> VisitsPoints { get; set; }
    public IEnumerable<PointResponse> ClientMembershipsPoints { get; set; }
}
