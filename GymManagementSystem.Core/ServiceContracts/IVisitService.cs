using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IVisitService
{
    Task<Result<Unit>> RegisterVisitAsync(Guid clientId);
}
