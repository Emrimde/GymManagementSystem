using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;

public abstract class BaseHttpClientService
{
    protected readonly HttpClient _httpClient;

    protected BaseHttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected BaseHttpClientService(HttpClient httpClient, AuthService authService)
    {
        _httpClient = httpClient;
    }

    // GET
    protected async Task<Result<TResponse>> GetAsync<TResponse>(string url)
    {
        var response = await _httpClient.GetAsync(url);
        return await HandleResponse<TResponse>(response);
    }

    // POST
    protected async Task<Result<TResponse>> PostAsync<TRequest, TResponse>(
        string url, TRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync(url, request);
        return await HandleResponse<TResponse>(response);
    }

    // PUT
    protected async Task<Result<TResponse>> PutAsync<TRequest, TResponse>(
        string url, TRequest request)
    {
        var response = await _httpClient.PutAsJsonAsync(url, request);
        return await HandleResponse<TResponse>(response);
    }

    protected async Task<Result<Unit>> DeleteAsync(string url)
    {
        var response = await _httpClient.DeleteAsync(url);
        return await HandleResponse<Unit>(response);
    }


    protected async Task<Result<TResponse>> PutAsync<TResponse>(string url)
    {
        var response = await _httpClient.PutAsync(url, null);
        return await HandleResponse<TResponse>(response);
    }

    // CORE
    private async Task<Result<T>> HandleResponse<T>(HttpResponseMessage response)
    {
        if (response.StatusCode == HttpStatusCode.NoContent)
            return Result<T>.Success(default!);

        string body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var data = string.IsNullOrWhiteSpace(body) ? default(T) : JsonSerializer.Deserialize<T>(body, JsonOptions());
            return Result<T>.Success(data!);
        }

        if (response.StatusCode == HttpStatusCode.BadRequest && !string.IsNullOrWhiteSpace(body))
        {
            var validation = JsonSerializer.Deserialize<ValidationProblemDetails>(body, JsonOptions());
            if (validation?.Errors?.Any() == true)
                return Result<T>.ValidationFailure(validation);
        }

        // --- specjalne: uwierzytelnianie / autoryzacja ---
        if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
        {
            // pusty body -> utwórz prosty ProblemDetails
            if (string.IsNullOrWhiteSpace(body))
            {
                return Result<T>.Failure(new ProblemDetails
                {
                    Status = (int)response.StatusCode,
                    Title = response.StatusCode == HttpStatusCode.Forbidden ? "Brak uprawnień" : "Nieautoryzowany"
                });
            }

            // jeśli body jest, spróbuj zdeserializować i ewentualnie wyciągnąć requiredRole
            var pd = JsonSerializer.Deserialize<ProblemDetails>(body, JsonOptions())
                     ?? new ProblemDetails { Status = (int)response.StatusCode };

            pd.Title ??= response.StatusCode == HttpStatusCode.Forbidden ? "Brak uprawnień" : "Nieautoryzowany";

            // parse "RequiredRole:Owner,Manager" w Detail albo Extensions["requiredRole"]
            if (!string.IsNullOrWhiteSpace(pd.Detail) && pd.Detail.Contains("RequiredRole:", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var part = pd.Detail.Split(new[] { "RequiredRole:" }, StringSplitOptions.RemoveEmptyEntries)[0].Length == 0
                        ? pd.Detail.Split("RequiredRole:")[1]
                        : pd.Detail.Split("RequiredRole:")[1];
                    var roles = part.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                                    .Select(r => r.Trim()).ToArray();
                    if (roles.Any()) pd.Extensions["requiredRole"] = roles;
                }
                catch { /* toleruj błędy parsowania */ }
            }

            return Result<T>.Failure(pd);
        }

        // jeśli body puste dla innych błędów -> generuj prosty ProblemDetails
        if (string.IsNullOrWhiteSpace(body))
        {
            return Result<T>.Failure(new ProblemDetails
            {
                Status = (int)response.StatusCode,
                Title = response.ReasonPhrase ?? "HTTP error"
            });
        }

        var problem = JsonSerializer.Deserialize<ProblemDetails>(body, JsonOptions())
                      ?? new ProblemDetails { Status = (int)response.StatusCode, Title = "HTTP error" };

        return Result<T>.Failure(problem);
    }

    private static JsonSerializerOptions JsonOptions() =>
        new() { PropertyNameCaseInsensitive = true };
}

