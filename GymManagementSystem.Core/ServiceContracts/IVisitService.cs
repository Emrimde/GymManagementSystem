using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IVisitService
{
    Task<Result<Unit>> DeleteVisitAsync(Guid visitId);
    Task<Result<IEnumerable<VisitResponse>>> GetAllClientVisitsAsync(Guid clientId);
    Task<Result<Unit>> RegisterVisitAsync(Guid clientId, string? guestName);
}
