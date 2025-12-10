using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface ITerminationService
{
    Task<PageResult<TerminationResponse>> GetAllAsync();
    Task<Result<TerminationResponse>> GetByIdAsync(Guid id);
    Task<Result<TerminationResponse>> CreateAsync(TerminationAddRequest entity);
}
