using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] SignInDto request) => HandleResult(await _authService.LoginAsync(request));

    [HttpPost("change-password")]
    public async Task<ActionResult> ChangePasswordForLoggedInUser([FromBody] ChangePasswordRequest request) => HandleResult(await _authService.ChangePasswordForLoggedInUserAsync(request));

    [HttpPost("activate-client-account")]
    public async Task<ActionResult> ActivateClientAccount([FromBody] ActivateAccountRequest request) => HandleResult(await _authService.ActivateClientAccountAsync(request));

    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword([FromBody] ForgotPasswordRequest request) => HandleResult(await _authService.ResetPasswordAsync(request));

    [HttpPost("reset-password-confirm")]
    public async Task<ActionResult> ResetPasswordConfirm([FromBody] ConfirmResetPasswordRequest request) => HandleResult(await _authService.ResetPasswordConfirmAsync(request));

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto request) => HandleResult(await _authService.RegisterAsync(request));
}
