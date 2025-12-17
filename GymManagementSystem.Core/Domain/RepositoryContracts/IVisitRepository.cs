using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.Domain.RepositoryContracts;

public interface IVisitRepository
{
    void AddVisit(Visit visit);
}
