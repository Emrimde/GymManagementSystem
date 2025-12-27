using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IAuthService
{
    Task<Result<AuthenticationResponse>> LoginAsync(SignInDto request);
    Task<Result<bool>> RegisterAsync(RegisterDto request);
}