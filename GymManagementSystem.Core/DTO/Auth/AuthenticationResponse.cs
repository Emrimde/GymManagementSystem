namespace GymManagementSystem.Core.DTO.Auth;
public class AuthenticationResponse
{
    public string Token { get; set; } = default!;
    public string? RefreshToken { get; set; } = default!;
    public bool MustChangePassword { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public DateTime? RefreshTokenExpirationDateTime { get; set; }
}
