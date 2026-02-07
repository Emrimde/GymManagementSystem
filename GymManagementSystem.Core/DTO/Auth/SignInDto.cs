namespace GymManagementSystem.Core.DTO.Auth;
public class SignInDto
{
    //public string? Username { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}