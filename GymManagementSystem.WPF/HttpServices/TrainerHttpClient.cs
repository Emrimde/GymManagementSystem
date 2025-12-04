using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Result;
using Microsoft.Extensions.Logging;
using Syncfusion.UI.Xaml.Scheduler;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Windows.Media;
using static System.Net.WebRequestMethods;

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
            if (trainer == null)
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

    public async Task<Result<TrainerTimeOff>> PostTrainerTimeOff(
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
            var data = JsonSerializer.Deserialize<TrainerTimeOff>(responseBody, options);
            return Result<TrainerTimeOff>.Success(data!);
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

                    return Result<TrainerTimeOff>.Failure(string.Join("\n", errorMessages!));
                }

                // Standard `ProblemDetails.detail`
                if (problem.TryGetValue("detail", out var detailElement))
                {
                    return Result<TrainerTimeOff>.Failure(detailElement.GetString()!);
                }
            }
        }
        catch { }

        return Result<TrainerTimeOff>.Failure($"HTTP Error {response.StatusCode}: {responseBody}");
    }

    public async Task<TrainerScheduleResponse> GetSchedule(Guid trainerId, int days = 30)
    {
        var response = await _httpClient.GetAsync(
            $"schedule/{trainerId}?days={days}");

        // możesz mieć swój Result pattern, to przykład:
        //response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TrainerScheduleResponse>();
    }

    public async Task<Result<TrainerTimeOff>> UpdateAsync(Guid id, TrainerTimeOffUpdateRequest dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"trainer-timeoff/{id}", dto);

        string body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return Result<TrainerTimeOff>.Failure(body);

        var data = JsonSerializer.Deserialize<TrainerTimeOff>(body, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return Result<TrainerTimeOff>.Success(data!);
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        var response = await _httpClient.DeleteAsync($"{id}");
        string body = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return Result<bool>.Failure(body);

        return Result<bool>.Success(true);
    }

    public async Task<Result<ObservableCollection<TrainerContractResponse>>> GetTrainerContracts()
    {
        try
        {
            ObservableCollection<TrainerContractResponse>? response = await _httpClient.GetFromJsonAsync<ObservableCollection<TrainerContractResponse>>
            ("trainercontracts");
            return Result<ObservableCollection<TrainerContractResponse>>.Success(response ?? new ObservableCollection<TrainerContractResponse>());

        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<TrainerContractResponse>>.Failure(ex.Message);
        }
    }

    public async Task<Result<ObservableCollection<TrainerContractInfoResponse>>> GetInstructors()
    {
        try
        {
            ObservableCollection<TrainerContractInfoResponse>? response = await _httpClient.GetFromJsonAsync<ObservableCollection<TrainerContractInfoResponse>>
            ("instructors");
            return Result<ObservableCollection<TrainerContractInfoResponse>>.Success(response ?? new ObservableCollection<TrainerContractInfoResponse>());

        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<TrainerContractInfoResponse>>.Failure(ex.Message);
        }
    }

    public async Task<Result<TrainerContractInfoResponse>> PostTrainerContractAsync(TrainerContractAddRequest request)
    {
        request.ValidFrom = request.ValidFrom?.ToUniversalTime();
        request.ValidTo = request.ValidTo?.ToUniversalTime();
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("trainercontract", request);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            TrainerContractInfoResponse? employee = JsonSerializer.Deserialize<TrainerContractInfoResponse>(responseBody);
            if (employee == null)
            {
                return Result<TrainerContractInfoResponse>.Failure("Unexpected employee");
            }
            return Result<TrainerContractInfoResponse>.Success(employee);
        }

        Dictionary<string, JsonElement>? errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
        if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
        {
            string errorMessage = detailElement.ToString();
            return Result<TrainerContractInfoResponse>.Failure(errorMessage);
        }
        if (errorDict != null && errorDict.TryGetValue("errors", out var errorsElement))
        {
            List<string> allErrors = new List<string>();
            foreach (var errorProp in errorsElement.EnumerateObject())
            {
                var messages = errorProp.Value.EnumerateArray();
                foreach (var item in messages)
                {
                    string msg = item.ToString();
                    allErrors.Add(msg);
                }
            }
            return Result<TrainerContractInfoResponse>.Failure(string.Join("\n", allErrors));
        }

        return Result<TrainerContractInfoResponse>.Failure("Something went wrong.");
    }

    public async Task<Result<TrainerContractDetailsResponse>> GetTrainerContractAsync(Guid trainerContractId, bool includeDetails)
    {
        try
        {
            TrainerContractDetailsResponse? trainer = await _httpClient.GetFromJsonAsync<TrainerContractDetailsResponse>($"trainercontract/{trainerContractId}?includeDetails={includeDetails}");

            if (trainer == null)
            {
                return Result<TrainerContractDetailsResponse>.Failure("Unexpected error");
            }
            return Result<TrainerContractDetailsResponse>.Success(trainer);

        }
        catch (HttpRequestException ex)
        {
            return Result<TrainerContractDetailsResponse>.Failure(ex.Message);
        }
    }
}
