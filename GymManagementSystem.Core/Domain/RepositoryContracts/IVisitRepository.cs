using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IVisitRepository
{
    void AddVisit(Visit visit);
    Task<int> GetTotalVisitsByClientId(Guid clientId);
    Task<DateTime> GetLastVisitDateByClientId(Guid clientId);
    Task<IEnumerable<VisitResponse>> GetAllClientVisits(Guid clientId);
    Task<int> GetTotalVisitsAsync(DateTime? date);
}
