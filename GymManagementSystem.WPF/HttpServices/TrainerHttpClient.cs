using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

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
}
