using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.DTO.Email;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.WebDTO.Auth;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IAuthService
{
    Task<Result<Unit>> ChangePasswordForLoggedInUserAsync(ChangePasswordRequest request);
    Task<Result<AuthenticationResponse>> LoginAsync(SignInDto request);
    Task<Result<bool>> RegisterAsync(RegisterDto request);
    Task<Result<Unit>> ResetPasswordAsync(ForgotPasswordRequest request);
}