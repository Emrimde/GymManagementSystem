
using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.WPF.HttpServices;

namespace GymManagementSystem.WPF.Services;
public class AuthService
{
    public string? JwtToken { get; private set; }
    public string? RefreshToken { get; private set; }
    public void SetProperty(string jwtToken) => JwtToken = jwtToken;
    public void SetTokens(string jwtToken, string refreshToken)
    {
        JwtToken = jwtToken;
        RefreshToken = refreshToken;
    }
    public void ClearJwt()
    {
        JwtToken = null;
        RefreshToken = null;
    }

    private readonly AuthHttpClient _authHttpClient;
    public AuthService(AuthHttpClient authHttpClient)
    {
        _authHttpClient = authHttpClient;
    }

    public async Task<bool> RefreshAsync()
    {
        if (string.IsNullOrEmpty(RefreshToken))
            return false;

        var result = await _authHttpClient.RefreshAsync(
            new RefreshTokenRequest
            {
                RefreshToken = RefreshToken
            });

        if (!result.IsSuccess || result.Value == null)
            return false;

        JwtToken = result.Value.Token;
        RefreshToken = result.Value.RefreshToken;

        return true;
    }
}
