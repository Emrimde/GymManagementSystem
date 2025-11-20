using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Services;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IAuthService
{
    Task<Result<bool>> LoginAsync(SignInDto request);
    Task<Result<bool>> RegisterAsync(RegisterDto request);
}