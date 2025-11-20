using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IServiceReader<Response>
{
    Task<Result<IEnumerable<Response>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<Response>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
