using GymManagementSystem.Core.DTO.Auth;

using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagementSystem.WPF.HttpServices;

public class AuthHttpClient : BaseHttpClientService
{
    public AuthHttpClient(HttpClient httpClient) : base(httpClient)
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
