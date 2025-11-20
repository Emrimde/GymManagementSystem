using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;

public interface ITerminationValidator
{
    Task<Result<bool>> CanCreateTerminationAsync(Guid clientId);
}
