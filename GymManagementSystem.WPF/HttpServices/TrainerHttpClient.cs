using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Enum;
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

            string errorMessage = responseBody; // fallback na cały responseBody

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<ObservableCollection<TrainerResponse>>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
                }
            }
            catch (JsonException)
            {
                // jeśli nie uda się zdeserializować JSON, zostaje cały responseBody
            }

            return Result<ObservableCollection<TrainerResponse>>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
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
            string errorMessage = responseBody; // fallback na cały responseBody

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<TrainerInfoResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
                }
            }
            catch (JsonException)
            {
                // jeśli nie uda się zdeserializować JSON, zostaje cały responseBody
            }

            return Result<TrainerInfoResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
        }
    }
}
