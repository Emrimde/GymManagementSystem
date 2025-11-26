using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface IService<Response, Request, UpdateRequest, Entity>
{
    Task<Result<Response>> CreateAsync(Request entity);
    Task<Result<IEnumerable<Response>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<Response>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<Response>> UpdateAsync(Guid id, UpdateRequest entity);
}