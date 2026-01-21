using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.Result;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;

public class GymClassHtppClient : BaseHttpClientService
{
    public GymClassHtppClient(HttpClient httpClient) : base(httpClient)
    {
    }
    public async Task<Result<ObservableCollection<GymClassResponse>>> GetGymClasses()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("");
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            ObservableCollection<GymClassResponse>? gymClasses = JsonSerializer.Deserialize<ObservableCollection<GymClassResponse>>(responseBody, jsonSerializerOptions);
            return Result<ObservableCollection<GymClassResponse>>.Success(gymClasses) ?? Result<ObservableCollection<GymClassResponse>>.Failure("Unexpected error during load gym classes");
        }
        else
        {

            string errorMessage = responseBody;

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<ObservableCollection<GymClassResponse>>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ObservableCollection<GymClassResponse>>.Failure($"Fatal error: {ex.Message}");
            }

            return Result<ObservableCollection<GymClassResponse>>.Failure(errorMessage);
        }
    }
    public async Task<Result<ObservableCollection<GymClassComboBoxResponse>>> GetGymClassComboBoxResponses()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("select-gymclasses");
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            ObservableCollection<GymClassComboBoxResponse>? gymClasses = JsonSerializer.Deserialize<ObservableCollection<GymClassComboBoxResponse>>(responseBody, jsonSerializerOptions);
            return Result<ObservableCollection<GymClassComboBoxResponse>>.Success(gymClasses) ?? Result<ObservableCollection<GymClassComboBoxResponse>>.Failure("Unexpected error during load gym classes");
        }
        else
        {

            string errorMessage = responseBody;

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<ObservableCollection<GymClassComboBoxResponse>>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ObservableCollection<GymClassComboBoxResponse>>.Failure($"Fatal error: {ex.Message}");
            }

            return Result<ObservableCollection<GymClassComboBoxResponse>>.Failure(errorMessage);
        }
    }

    public async Task<Result<GymClassForEditResponse>> GetGymClassForEdit(Guid gymClassId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"get-gymclass-for-edit/{gymClassId}");
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            GymClassForEditResponse? gymClasses = JsonSerializer.Deserialize<GymClassForEditResponse>(responseBody, jsonSerializerOptions);
            return Result<GymClassForEditResponse>.Success(gymClasses) ?? Result<GymClassForEditResponse>.Failure("Unexpected error during load gym class");
        }
        else
        {

            string errorMessage = responseBody;

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<GymClassForEditResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<GymClassForEditResponse>.Failure($"Fatal error: {ex.Message}");
            }

            return Result<GymClassForEditResponse>.Failure(errorMessage);
        }
    }

    public async Task<Result<GymClassInfoResponse>> PostGymClassAsync(GymClassAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            GymClassInfoResponse? trainer = JsonSerializer.Deserialize<GymClassInfoResponse>(responseBody, options);
            return Result<GymClassInfoResponse>.Success(trainer!);
        }
        else
        {
            string errorMessage = responseBody;

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<GymClassInfoResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<GymClassInfoResponse>.Failure($"Fatal error {ex.Message}");
            }

            return Result<GymClassInfoResponse>.Failure(errorMessage);
        }
    }


    public async Task<Result<Unit>> GenerateNewScheduledClasses(Guid gymClassId)
    {
        HttpResponseMessage response = await _httpClient.PostAsync($"{gymClassId}", null!);
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return Result<Unit>.Success(new Unit());
        }
        else
        {
            string errorMessage = responseBody;

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<Unit>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure($"Fatal error {ex.Message}");
            }

            return Result<Unit>.Failure(errorMessage);
        }
    }

    public async Task<Result<Unit>> PutGymClassAsync(GymClassUpdateRequest gymClassUpdateRequest)
    {
        string json = JsonSerializer.Serialize(gymClassUpdateRequest);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync("", content);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return Result<Unit>.Success(new Unit());
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

                return Result<Unit>.Failure(message);
            }

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<Unit>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<Unit>.Failure($"Fatal error {ex.Message}");
            }

            return Result<Unit>.Failure(errorMessage);
        }
    }
}
