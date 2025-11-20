using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] SignInDto request)
    {
        Result<bool> result = await _authService.LoginAsync(request);

        if (!result.IsSuccess)
        {
            return Problem(detail: result.ErrorMessage, statusCode: (int)result.StatusCode);
        }
        return Ok();
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDto request)
    {
        Result<bool> result = await _authService.RegisterAsync(request);
        if (!result.IsSuccess)
        {
            return Problem(detail: result.ErrorMessage, statusCode: (int)result.StatusCode);
        }
        return Ok();
    }
}
