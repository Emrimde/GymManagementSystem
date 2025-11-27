using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerAvailabilityTemplate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Result;
using Microsoft.Extensions.Logging;
using Syncfusion.UI.Xaml.Scheduler;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Media;

namespace GymManagementSystem.WPF.HttpServices;
public class TrainerHttpClient : BaseHttpClientService
{
    public TrainerHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
    public async Task<Result<ObservableCollection<TrainerResponse>>> GetTrainers()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("");
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            ObservableCollection<TrainerResponse>? trainers = JsonSerializer.Deserialize<ObservableCollection<TrainerResponse>>(responseBody, jsonSerializerOptions);
            return Result<ObservableCollection<TrainerResponse>>.Success(trainers) ?? Result<ObservableCollection<TrainerResponse>>.Failure("");
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
                    return Result<ObservableCollection<TrainerResponse>>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ObservableCollection<TrainerResponse>>.Failure($"Fatal error {ex.Message}");
            }

            return Result<ObservableCollection<TrainerResponse>>.Failure(errorMessage);
        }
    }

    public async Task<Result<TrainerInfoResponse>> PostTrainerAsync(TrainerAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            TrainerInfoResponse? trainer = JsonSerializer.Deserialize<TrainerInfoResponse>(responseBody, options);
            return Result<TrainerInfoResponse>.Success(trainer!);
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
                    return Result<TrainerInfoResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<TrainerInfoResponse>.Failure($"Fatal error {ex.Message}");
            }

            return Result<TrainerInfoResponse>.Failure(errorMessage);
        }
    }

    public async Task<Result<TrainerDetailsResponse>> GetTrainer(Guid trainerId)
    {
        try
        {
            TrainerDetailsResponse? trainer = await _httpClient.GetFromJsonAsync<TrainerDetailsResponse>($"{trainerId}");
            if(trainer == null)
            {
                return Result<TrainerDetailsResponse>.Failure("Unexpected error");
            }
            return Result<TrainerDetailsResponse>.Success(trainer);
            
        }
        catch (HttpRequestException ex)
        {
            return Result<TrainerDetailsResponse>.Failure(ex.Message);
        }
    }

    public async Task<Result<TrainerAvailabilityInfoResponse>> PostTrainerAvailabilityAsync(
     TrainerAvailabilityAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("availability-template", request);
        string responseBody = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (response.IsSuccessStatusCode)
        {
            var data = JsonSerializer.Deserialize<TrainerAvailabilityInfoResponse>(responseBody, options);
            return Result<TrainerAvailabilityInfoResponse>.Success(data!);
        }

        // Obsługa ProblemDetails (detail)
        try
        {
            var problem = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody, options);

            if (problem != null)
            {
                // Błędy walidacyjne (.NET standard 422)
                if (problem.TryGetValue("errors", out var errorsElement))
                {
                    var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsElement.GetRawText(), options);
                    var errorMessages = errors?
                        .SelectMany(e => e.Value)
                        .ToList();

                    return Result<TrainerAvailabilityInfoResponse>.Failure(string.Join("\n", errorMessages!));
                }

                // Standard `ProblemDetails.detail`
                if (problem.TryGetValue("detail", out var detailElement))
                {
                    return Result<TrainerAvailabilityInfoResponse>.Failure(detailElement.GetString()!);
                }
            }
        }
        catch { }

        return Result<TrainerAvailabilityInfoResponse>.Failure($"HTTP Error {response.StatusCode}: {responseBody}");
    }
    public async Task<Result<TrainerTimeOffInfoResponse>> PostTrainerTimeOff(
     TrainerTimeOffAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("timeoff", request);
        string responseBody = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        if (response.IsSuccessStatusCode)
        {
            var data = JsonSerializer.Deserialize<TrainerTimeOffInfoResponse>(responseBody, options);
            return Result<TrainerTimeOffInfoResponse>.Success(data!);
        }

        // Obsługa ProblemDetails (detail)
        try
        {
            var problem = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody, options);

            if (problem != null)
            {
                // Błędy walidacyjne (.NET standard 422)
                if (problem.TryGetValue("errors", out var errorsElement))
                {
                    var errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(errorsElement.GetRawText(), options);
                    var errorMessages = errors?
                        .SelectMany(e => e.Value)
                        .ToList();

                    return Result<TrainerTimeOffInfoResponse>.Failure(string.Join("\n", errorMessages!));
                }

                // Standard `ProblemDetails.detail`
                if (problem.TryGetValue("detail", out var detailElement))
                {
                    return Result<TrainerTimeOffInfoResponse>.Failure(detailElement.GetString()!);
                }
            }
        }
        catch { }

        return Result<TrainerTimeOffInfoResponse>.Failure($"HTTP Error {response.StatusCode}: {responseBody}");
    }

    public async Task<TrainerScheduleResponse> GetSchedule(Guid trainerId, int days = 30)
    {
        var response = await _httpClient.GetAsync(
            $"schedule/{trainerId}?days={days}");
        
        // możesz mieć swój Result pattern, to przykład:
        //response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TrainerScheduleResponse>();
    }


}
