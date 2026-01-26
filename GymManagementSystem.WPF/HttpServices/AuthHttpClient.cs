using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.WPF.Result;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class AuthHttpClient : BaseHttpClientService
{
    public AuthHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public async Task<Result<AuthenticationResponse>> LoginAsync(SignInDto signInDto)
    {
        return await PostAsync<SignInDto, AuthenticationResponse>(
            "login",
            signInDto
        );
    }

    public async Task<Result<Unit>> RegisterAsync(RegisterDto registerDto)
    {
        return await PostAsync<RegisterDto, Unit>(
            "register",
            registerDto
        );
    }
}
