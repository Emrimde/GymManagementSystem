namespace GymManagementSystem.WPF.Services;
public class AuthService
{
    public string? JwtToken { get; private set; }
    public void SetProperty(string jwtToken) => JwtToken = jwtToken;
    public void ClearJwt() => JwtToken = null;
}
