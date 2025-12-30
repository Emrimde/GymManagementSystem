using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Core.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
    }
    public async Task<Result<AuthenticationResponse>> LoginAsync(SignInDto request)
    {
        var user = request.Email != null
            ? await _userManager.FindByEmailAsync(request.Email)
            : await _userManager.FindByNameAsync(request.Username);

        if (user is null)
            return Result<AuthenticationResponse>.Failure("Invalid username or password", StatusCodeEnum.Unauthorized);

        var passwordOk = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordOk)
            return Result<AuthenticationResponse>.Failure("Invalid username or password", StatusCodeEnum.Unauthorized);

        // ⬇️ TYLKO TERAZ generujesz JWT
        var token = await _jwtService.CreateJwtToken(user);

        return Result<AuthenticationResponse>.Success(token, StatusCodeEnum.Ok);
    }

    public async Task<Result<bool>> RegisterAsync(RegisterDto request)
    {
        
        if (await _userManager.FindByNameAsync(request.Username) != null)
        {
            return Result<bool>.Failure("Username already in use", StatusCodeEnum.BadRequest);
        }

        if (await _userManager.FindByEmailAsync(request.Email) != null)
        {
            return Result<bool>.Failure("Email already in use", StatusCodeEnum.BadRequest);
        }

        User user = new User
        {
            UserName = request.Username,
            Email = request.Email,
        };

        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        await _userManager.AddToRoleAsync(user, "");

        if (!result.Succeeded)
        {
            return Result<bool>.Failure(string.Join(", ", result.Errors.Select(item => item.Description)), StatusCodeEnum.Unauthorized);
        }

        return Result<bool>.Success(true, StatusCodeEnum.Ok);
    }
}
