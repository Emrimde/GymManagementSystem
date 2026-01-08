using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.ClientMembership;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GymManagementSystem.Core.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _contextAccessor = contextAccessor;
    }

    public async Task<Result<Unit>> ChangePasswordForLoggedInUserAsync(ChangePasswordRequest request)
    {
        string? userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Result<Unit>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
        }
        User? user = await _userManager.FindByIdAsync(userId);
        if(user == null)
        {
            return Result<Unit>.Failure("User not found", StatusCodeEnum.NotFound);
        }

        if(request.CurrentPassword == request.NewPassword)
        {
            return Result<Unit>.Failure("New password is the same as old password", StatusCodeEnum.BadRequest);
        }

        IdentityResult identityResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if(identityResult != IdentityResult.Success)
        {
            return Result<Unit>.Failure("Error during changing password", StatusCodeEnum.InternalServerError);
        }

        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Unauthorized);
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
