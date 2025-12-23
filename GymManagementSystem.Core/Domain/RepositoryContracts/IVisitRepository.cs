using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.DTO.Dashboard;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IVisitRepository
{
    void AddVisit(Visit visit);
    Task<int> GetTotalVisitsByClientId(Guid clientId);
    Task<DateTime> GetLastVisitDateByClientId(Guid clientId);
    Task<IEnumerable<VisitResponse>> GetAllClientVisits(Guid clientId);
    Task<int> GetTotalVisitsAsync(DateTime? date);
    Task<IEnumerable<PointResponse>> GetAllVisitsOverTime(DateTime startTime, DateTime endTime);
}
