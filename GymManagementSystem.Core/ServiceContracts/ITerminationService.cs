using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface ITerminationService
{
    Task<Result<TerminationResponse>> CreateAsync(TerminationAddRequest entity);
}
