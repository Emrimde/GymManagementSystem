using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IVisitService
{
    Task<Result<IEnumerable<VisitResponse>>> GetAllClientVisitsAsync(Guid clientId);
    Task<Result<Unit>> RegisterVisitAsync(Guid clientId);
}
