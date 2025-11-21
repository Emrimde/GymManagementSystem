using GymManagementSystem.Core.DTO.GymClass;
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
}
