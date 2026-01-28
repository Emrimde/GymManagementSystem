namespace GymManagementSystem.Core.DTO.Dashboard;
public class DashboardPlotsDataResponse
{
    public IEnumerable<PointResponse> VisitsPoints { get; set; } = new List<PointResponse>();
    public IEnumerable<PointResponse> ClientMembershipsPoints { get; set; } = new List<PointResponse>();
}
