using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.DTO.Email;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Policies;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.WebDTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace GymManagementSystem.Core.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, IJwtService jwtService, IHttpContextAccessor contextAccessor, IEmailService emailService, IConfiguration configuration)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _contextAccessor = contextAccessor;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<Result<Unit>> ChangePasswordForLoggedInUserAsync(ChangePasswordRequest request)
    {
        string? userId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Result<Unit>.Failure("Error, token not found", StatusCodeEnum.Unauthorized);
        }
        User? user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result<Unit>.Failure("User not found", StatusCodeEnum.NotFound);
        }

        IdentityResult identityResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!identityResult.Succeeded)
        {
            string error = identityResult.Errors.FirstOrDefault()?.Description ?? "Password change failed";
            return Result<Unit>.Failure(error, StatusCodeEnum.BadRequest);
        }

        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> ResetPasswordAsync(ForgotPasswordRequest request)
    {
        User? user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
        }
        string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        string encodedToken = Uri.EscapeDataString(resetPasswordToken);
        string userId = user.Id.ToString();

        string link =
            $"{_configuration["App:WebUrl"]}/reset-client-password" +
            $"?userId={userId}&token={encodedToken}";

        EmailRequest emailRequest = new EmailRequest
        {
            To = user.Email!,
            Subject = "Reset your password",
            Body = $@"
        <p>Click the link to reset your password. If it's not you, ignore message.</p>
        <p>
            <a href='{link}'>Reset Password</a>
        </p>
       "
        };

        await _emailService.SendLink(emailRequest);
        return Result<Unit>.Success(new Unit(), StatusCodeEnum.Ok);
    }

    public async Task<Result<AuthenticationResponse>> LoginAsync(SignInDto request)
    {
        User? user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return Result<AuthenticationResponse>.Failure("Invalid username or password", StatusCodeEnum.Unauthorized);
        }


        var passwordOk = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordOk)
        {
            return Result<AuthenticationResponse>.Failure("Invalid username or password", StatusCodeEnum.Unauthorized);
        }

        IList<string> roles = await _userManager.GetRolesAsync(user);

        if (!ApplicationAccessPolicy.CanAccess(request.AppType, roles))
        {
            return Result<AuthenticationResponse>.Failure("Invalid username or password", StatusCodeEnum.Unauthorized);
        }

        AuthenticationResponse token = await _jwtService.CreateJwtToken(user);
        token.MustChangePassword = user.MustChangePassword;

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

    public async Task<Result<Unit>> ActivateClientAccountAsync(ActivateAccountRequest request)
    {
        User? user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return Result<Unit>.Failure("User not found", StatusCodeEnum.NotFound);
        }

        if (user.EmailConfirmed)
        {
            return Result<Unit>.Failure("Account already activated", StatusCodeEnum.BadRequest);
        }

        IdentityResult result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!result.Succeeded)
        {
            string error = string.Join('\n', result.Errors.Select(item => item.Description));
            return Result<Unit>.Failure(error, StatusCodeEnum.BadRequest);
        }

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);

        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> ForceChangePasswordAsync(ForceChangePasswordRequest request)
    {
        string? userId = _contextAccessor.HttpContext?
            .User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return Result<Unit>.Failure("Unauthorized", StatusCodeEnum.Unauthorized);
        }

        User? user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return Result<Unit>.Failure("User not found", StatusCodeEnum.NotFound);
        }

        if (!user.MustChangePassword)
        {
            return Result<Unit>.Failure("Password change not required", StatusCodeEnum.BadRequest);
        }

        IdentityResult removeResult = await _userManager.RemovePasswordAsync(user);
        if (!removeResult.Succeeded)
        {
            return Result<Unit>.Failure("Error removing password", StatusCodeEnum.InternalServerError);
        }

        IdentityResult addResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
        if (!addResult.Succeeded)
        {
            return Result<Unit>.Failure("Error setting new password", StatusCodeEnum.InternalServerError);
        }

        user.MustChangePassword = false;
        await _userManager.UpdateAsync(user);

        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.Ok);
    }

    public async Task<Result<Unit>> SetNewPasswordAsync(SetNewPasswordRequest request)
    {
        var user = await _userManager.Users
       .SingleOrDefaultAsync(item => item.PersonId == request.PersonId);

        if (user == null)
        {
            return Result<Unit>.Failure("Employee not found");
        }


        IdentityResult removeResult = await _userManager.RemovePasswordAsync(user);
        if (!removeResult.Succeeded)
        {
            return Result<Unit>.Failure(
                string.Join(", ", removeResult.Errors.Select(item => item.Description))
            );
        }

        IdentityResult addResult = await _userManager.AddPasswordAsync(
            user,
            request.NewPassword
        );

        if (!addResult.Succeeded)
        {
            return Result<Unit>.Failure(
                string.Join(", ", addResult.Errors.Select(item => item.Description))
            );
        }

        user.MustChangePassword = false;
        IdentityResult updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return Result<Unit>.Failure(
                string.Join(", ", updateResult.Errors.Select(e => e.Description))
            );
        }

        await _userManager.UpdateSecurityStampAsync(user);
        return Result<Unit>.Success(Unit.Value, StatusCodeEnum.Ok);
    }
}
