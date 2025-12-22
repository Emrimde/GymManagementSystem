using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Result;

namespace GymManagementSystem.WPF.HttpServices;
public class TrainerHttpClient : BaseHttpClientService
{
    public TrainerHttpClient(HttpClient httpClient) : base(httpClient)
    {
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

    public async Task<PageResult<TrainerContractResponse>> GetTrainerContracts(string? searchText, int page = 1)
    {
        string query = string.IsNullOrWhiteSpace(searchText) ? $"trainercontracts?page={page}" : $"trainercontracts?searchText={Uri.UnescapeDataString(searchText)}&page={page}";
        try
        {
            PageResult<TrainerContractResponse>? response = await _httpClient.GetFromJsonAsync<PageResult<TrainerContractResponse>>(query);
            return response;

        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show("Failed to load trainers.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return new PageResult<TrainerContractResponse>();
        }
    }
    public async Task<Result<ObservableCollection<TrainerRateResponse>>> GetTrainerRatesAsync(Guid id)
    {
        try
        {
            ObservableCollection<TrainerRateResponse>? response = await _httpClient.GetFromJsonAsync<ObservableCollection<TrainerRateResponse>>
            ($"trainer-rates/{id}");
            return Result<ObservableCollection<TrainerRateResponse>>.Success(response ?? new ObservableCollection<TrainerRateResponse>());

        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<TrainerRateResponse>>.Failure(ex.Message);
        }
    }

    public async Task<Result<ObservableCollection<TrainerRateSelectResponse>>> GetTrainerRatesSelectAsync(Guid id)
    {
        try
        {

            ObservableCollection<TrainerRateSelectResponse>? response = await _httpClient.GetFromJsonAsync<ObservableCollection<TrainerRateSelectResponse>>
            ($"trainer-rates-select/{id}");
            return Result<ObservableCollection<TrainerRateSelectResponse>>.Success(response ?? new ObservableCollection<TrainerRateSelectResponse>());

        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<TrainerRateSelectResponse>>.Failure(ex.Message);
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
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("trainercontract", request);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            TrainerContractInfoResponse? employee = JsonSerializer.Deserialize<TrainerContractInfoResponse>(responseBody, jsonSerializerOptions);
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

    public async Task<Result<TrainerRateInfoResponse>> AddTrainerRateAsync(TrainerRateAddRequest request)
    {
        request.ValidFrom = request.ValidFrom.ToUniversalTime();
     
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("trainer-rate", request);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            TrainerRateInfoResponse? employee = JsonSerializer.Deserialize<TrainerRateInfoResponse>(responseBody);
            if (employee == null)
            {
                return Result<TrainerRateInfoResponse>.Failure("Unexpected employee");
            }
            return Result<TrainerRateInfoResponse>.Success(employee);
        }

        Dictionary<string, JsonElement>? errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
        if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
        {
            string errorMessage = detailElement.ToString();
            return Result<TrainerRateInfoResponse>.Failure(errorMessage);
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
            return Result<TrainerRateInfoResponse>.Failure(string.Join("\n", allErrors));
        }

        return Result<TrainerRateInfoResponse>.Failure("Something went wrong.");
    }
}
