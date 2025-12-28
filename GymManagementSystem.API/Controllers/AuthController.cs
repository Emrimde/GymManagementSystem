using GymManagementSystem.API.Controllers.Base;
using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.ServiceContracts;
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

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto request) => HandleResult(await _authService.RegisterAsync(request));
}
