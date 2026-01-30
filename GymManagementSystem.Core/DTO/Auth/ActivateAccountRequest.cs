namespace GymManagementSystem.Core.DTO.Auth;
public class ActivateAccountRequest
{
    public string UserId { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
