using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;

public class AuthHttpClient : BaseHttpClientService
{
    public AuthHttpClient(HttpClient httpClient) : base(httpClient)
    {

    }
    public async Task<Result<AuthenticationResponse>> LoginAsync(SignInDto signInDto)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("login", signInDto);

        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var authenticationResponse = await response.Content.ReadFromJsonAsync<AuthenticationResponse>();
            if (authenticationResponse == null)
                return Result<AuthenticationResponse>.Failure("Empty response from server.");

            return Result<AuthenticationResponse>.Success(authenticationResponse);
        }


        try
        {
            var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);

            if (errorDict != null)
            {
                if (errorDict.TryGetValue("detail", out var detail))
                    return Result<AuthenticationResponse>.Failure(detail.GetString() ?? "Unknown error.");

                // Obsługa błędów walidacji
                if (errorDict.TryGetValue("errors", out var errorsElement))
                {
                    var errors = errorsElement.EnumerateObject()
                        .SelectMany(e => e.Value.EnumerateArray().Select(item => item.GetString()))
                        .Where(item => !string.IsNullOrEmpty(item));

                    return Result<AuthenticationResponse>.Failure(string.Join("\n", errors));
                }
            }
        }
        catch
        {
            
        }

        return Result<AuthenticationResponse>.Failure(response.ReasonPhrase ?? "Unknown error.");
    }

    public async Task<HttpResponseMessage> RegisterAsync(RegisterDto registerDto)
    {
        return await _httpClient.PostAsJsonAsync("register", registerDto);
    }
}
