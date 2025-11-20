using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
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
            return Result<ObservableCollection<GymClassResponse>>.Success(gymClasses) ?? Result<ObservableCollection<GymClassResponse>>.Failure("");
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
                    return Result<ObservableCollection<GymClassResponse>>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
                }
            }
            catch (JsonException)
            {
                // jeśli nie uda się zdeserializować JSON, zostaje cały responseBody
            }

            return Result<ObservableCollection<GymClassResponse>>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
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
            string errorMessage = responseBody; // fallback na cały responseBody

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<GymClassInfoResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
                }
            }
            catch (JsonException)
            {
                // jeśli nie uda się zdeserializować JSON, zostaje cały responseBody
            }

            return Result<GymClassInfoResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
        }
    }
}
