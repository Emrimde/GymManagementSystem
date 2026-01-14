using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.DTO.Email;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GymManagementSystem.Core.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IEmailService _emailService;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IHttpContextAccessor contextAccessor, IEmailService emailService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _contextAccessor = contextAccessor;
        _emailService = emailService;
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

    public async Task<Result<Unit>> ResetPasswordAsync(ForgotPasswordRequest request)
    {
        User? user = await _userManager.FindByEmailAsync(request.Email); 
        if(user == null)
        {
           return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
        }
        string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        string encodedToken = Uri.EscapeDataString(resetPasswordToken);
        string userId = user.Id.ToString();
        EmailRequest emailRequest = new EmailRequest
        {
            To = request.Email,
            Body = $"Click the link to reset your password. If it's not you, ignore message <a href='https://yourfrontend.com/reset-password?token={encodedToken}&userId={userId}'>Reset Password</a> <br>" +
            $"token = CfDJ8CGDZJLEgY9HruVUevfK%2FSeKneozyGZ8i3I7CDzd1UevaWm0u7RMCRLuzjf7VA6%2BjYCyqUl%2Bv%2FvN%2Fg4vnT778CxD2KiBU5J3g1FvZhPdD99zftSzbk10KCN5vBF7ffkidBapk4hcRPPfA6UchdwSai7g7zdB%2FSXfY6Vi%2B7PjpY33%2BK36h%2FDgYOv50C0gcACrKGrowMGBM4WP9fmSNC5J1Se1eHE6FCA6JQowTWGm78BR&userId=fe48d75f-4d8d-4aaa-8d68-b2efb12af6bc"
            ,
            Subject = "Password Reset"
        };

        await _emailService.SendLink(emailRequest);
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
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

    public async Task<Result<Unit>> ResetPasswordConfirmAsync(ConfirmResetPasswordRequest request)
    {
        var token = Uri.UnescapeDataString(request.Token);
        User? user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return Result<Unit>.Failure("Error", StatusCodeEnum.BadRequest);
        }

        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
        if (!result.Succeeded)
        {
            string errorMessage = string.Join(" , ", result.Errors.Select(item => item.Description));
            return Result<Unit>.Failure($"{errorMessage}", StatusCodeEnum.BadRequest);
        }

        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }
}
