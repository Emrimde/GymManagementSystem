namespace GymManagementSystem.Core.WebDTO.Auth;
public class ConfirmResetPasswordRequest
{
    public string UserId { get; set; } = default!;
    public string Token { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}
