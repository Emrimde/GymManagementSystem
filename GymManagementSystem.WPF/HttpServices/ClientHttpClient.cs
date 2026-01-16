using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;

public class ClientHttpClient : BaseHttpClientService
{
    public ClientHttpClient(HttpClient httpClient) : base(httpClient)
    {

    }

    public async Task<Result<ClientAgeValidationResponse>> ValidateClientAgeAsync(
     ClientAgeValidationRequest request)
    {
        try
        {
            var httpResponse = await _httpClient.PostAsJsonAsync($"validate", request);

            if (!httpResponse.IsSuccessStatusCode)
            {
                return Result<ClientAgeValidationResponse>.Failure(
                    $"Validation failed with status {httpResponse.StatusCode}");
            }

            var response = await httpResponse.Content.ReadFromJsonAsync<ClientAgeValidationResponse>();

            if (response is null)
            {
                return Result<ClientAgeValidationResponse>.Failure("Empty response from server.");
            }

            return Result<ClientAgeValidationResponse>.Success(response);
        }
        catch (HttpRequestException ex)
        {
            return Result<ClientAgeValidationResponse>.Failure(
                $"Error validating client age: {ex.Message}");
        }
    }

    public async Task<ClientInfoResponse> GetClientNameById(Guid clientId)
    {
        try
        {
            ClientInfoResponse? clientNameResponse = await _httpClient.GetFromJsonAsync<ClientInfoResponse>($"name/{clientId}");
            return clientNameResponse ?? new ClientInfoResponse();
        }
        catch (HttpRequestException)
        {
            return null!;
        }

    }

    public async Task<PageResult<ClientResponse>> GetAllClientsAsync(string? searchText, int page)
    {
        string query = string.IsNullOrWhiteSpace(searchText) ? $"?page={page}" : $"?searchText={Uri.UnescapeDataString(searchText)}&page={page}";
        HttpResponseMessage response = await _httpClient.GetAsync(query);
        if (response.IsSuccessStatusCode)
        {
            PageResult<ClientResponse>? clients = await response.Content.ReadFromJsonAsync<PageResult<ClientResponse>>();
            return clients ?? new PageResult<ClientResponse>();
        }
        else
        {
            var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            if (problem?.Errors != null)
            {
                var errors = problem.Errors
                    .SelectMany(e => e.Value.Select(msg => $"{e.Key}: {msg}"));

                string message = string.Join("\n", errors);

                MessageBox.Show(message, "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("Request failed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return new PageResult<ClientResponse>();
        }
    }

    public async Task<Result<ClientInfoResponse>> PostClientAsync(ClientAddRequest request)
    {
        request.DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Utc); // był błąd z postgresql, strefy czasowe
        string json = JsonSerializer.Serialize(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync("", content);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions // backend zwracał camelCase
            {
                PropertyNameCaseInsensitive = true
            };
            ClientInfoResponse? client = JsonSerializer.Deserialize<ClientInfoResponse>(responseBody, options);
            return Result<ClientInfoResponse>.Success(client!);
        }
        else
        {

            string errorMessage = responseBody;
            ValidationProblemDetails? problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            if (problem?.Errors != null)
            {
                var errors = problem.Errors
                    .SelectMany(item => item.Value.Select(msg => $"{item.Key}: {msg}"));

                string message = string.Join("\n", errors);

                return Result<ClientInfoResponse>.Failure(message);
            }

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<ClientInfoResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ClientInfoResponse>.Failure($"Fatal error {ex.Message}");
            }

            return Result<ClientInfoResponse>.Failure(errorMessage);
        }
    }

    public async Task<Result<ClientInfoResponse>> PutClientAsync(ClientUpdateRequest request, Guid id)
    {
        string json = JsonSerializer.Serialize(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync($"{id}", content);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            };
            ClientInfoResponse? client = JsonSerializer.Deserialize<ClientInfoResponse>(responseBody, options);
            return Result<ClientInfoResponse>.Success(client!);
        }

        else
        {
            string errorMessage = responseBody;
            ValidationProblemDetails? problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            if (problem?.Errors != null)
            {
                var errors = problem.Errors
                    .SelectMany(item => item.Value.Select(msg => $"{item.Key}: {msg}"));

                string message = string.Join("\n", errors);

                return Result<ClientInfoResponse>.Failure(message);
            }
            
            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<ClientInfoResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ClientInfoResponse>.Failure($"Fatal error {ex.Message}");
            }

            return Result<ClientInfoResponse>.Failure(errorMessage);
        }
    }

    public async Task<Result<ClientDetailsResponse>> GetClientById(Guid id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{id}");
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            ClientDetailsResponse? client = await response.Content.ReadFromJsonAsync<ClientDetailsResponse>();
            return Result<ClientDetailsResponse>.Success(client!) ?? Result<ClientDetailsResponse>.Failure("Client details not found");
        }

        else
        {
            string errorMessage = responseBody;
            ValidationProblemDetails? problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            if (problem?.Errors != null)
            {
                var errors = problem.Errors
                    .SelectMany(e => e.Value.Select(msg => $"{e.Key}: {msg}"));

                string message = string.Join("\n", errors);

               return Result<ClientDetailsResponse>.Failure(message);
            }

            else
            {
                try
                {
                    var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                    if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                    {
                        errorMessage = detailElement.GetString() ?? responseBody;
                        return Result<ClientDetailsResponse>.Failure(errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    return Result<ClientDetailsResponse>.Failure($"Fatal rror {ex.Message}");
                }
            }

            return Result<ClientDetailsResponse>.Failure(errorMessage);
        }
    }

    public async Task<Result<ClientEditResponse>> GetClientForEditByClientIdAsync(Guid id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"get-for-edit/{id}");
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            ClientEditResponse? client = await response.Content.ReadFromJsonAsync<ClientEditResponse>();
            return Result<ClientEditResponse>.Success(client!) ?? Result<ClientEditResponse>.Failure("Client details not found");
        }

        else
        {
            string errorMessage = responseBody;
            ValidationProblemDetails? problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
            if (problem?.Errors != null)
            {
                var errors = problem.Errors
                    .SelectMany(e => e.Value.Select(msg => $"{e.Key}: {msg}"));

                string message = string.Join("\n", errors);

                return Result<ClientEditResponse>.Failure(message);
            }

            else
            {
                try
                {
                    var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                    if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                    {
                        errorMessage = detailElement.GetString() ?? responseBody;
                        return Result<ClientEditResponse>.Failure(errorMessage);
                    }
                }
                catch (Exception ex)
                {
                    return Result<ClientEditResponse>.Failure($"Fatal rror {ex.Message}");
                }
            }

            return Result<ClientEditResponse>.Failure(errorMessage);
        }
    }


    public async Task<Result<IEnumerable<ClientInfoResponse>>> LookUpClients(string query, Guid? scheduledClassId)
    {
        try
        {
            var url =
                $"lookup?query={Uri.EscapeDataString(query)}&scheduledClassId={scheduledClassId}";

            var clients = await _httpClient.GetFromJsonAsync<IEnumerable<ClientInfoResponse>>(url);

            return Result<IEnumerable<ClientInfoResponse>>.Success(
                clients ?? Enumerable.Empty<ClientInfoResponse>());
        }
        catch (HttpRequestException ex)
        {
            return Result<IEnumerable<ClientInfoResponse>>.Failure(ex.Message);
        }
    }
}
