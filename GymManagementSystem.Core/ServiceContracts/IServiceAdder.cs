using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts
{
    public interface IServiceAdder<TResponse, TRequest>
    {
        Task<Result<TResponse>> CreateAsync(TRequest entity, CancellationToken cancellationToken);
    }
}
