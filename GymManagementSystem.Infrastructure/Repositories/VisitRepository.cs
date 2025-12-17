using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;

namespace GymManagementSystem.Infrastructure.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly ApplicationDbContext _dbContext;
    public VisitRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void AddVisit(Visit visit)
    {
        _dbContext.Visits.Add(visit);
    }
}
