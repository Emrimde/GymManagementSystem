using GymManagementSystem.Core.DTO.Auth;

using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagementSystem.Desktop.Services;

public class AuthService
{
    public AuthService(HttpClient httpClient) 
    {

    }

    public async Task<HttpResponseMessage> LoginAsync(SignInDto signInDto)
    {
        return await _httpClient.PostAsJsonAsync("login", signInDto);
    }

    public async Task<HttpResponseMessage> RegisterAsync(RegisterDto registerDto)
    {
        return await _httpClient.PostAsJsonAsync("register", registerDto);
    }
}
