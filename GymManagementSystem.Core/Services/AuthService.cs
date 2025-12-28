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
    public  async Task<Result<AuthenticationResponse>> LoginAsync(SignInDto request)
    {
        User? user;
        if (request.Email != null)
        {
            user = await _userManager.FindByEmailAsync(request.Email);
        }
        else
        {
            user = await _userManager.FindByNameAsync(request.Username);
        }

        if(user == null)
        {
            return Result<AuthenticationResponse>.Failure("Invalid username or password", StatusCodeEnum.Unauthorized);
        }
        
        SignInResult result = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);
        AuthenticationResponse authenticationResponse = await _jwtService.CreateJwtToken(user);
        if (!result.Succeeded)
        {
            return Result<AuthenticationResponse>.Failure("Invalid username or password", StatusCodeEnum.Unauthorized);
        }

        return Result<AuthenticationResponse>.Success(authenticationResponse, StatusCodeEnum.Ok);
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
