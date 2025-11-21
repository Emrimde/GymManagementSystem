using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;
public class ScheduledClassHttpClient : BaseHttpClientService
{
    public ScheduledClassHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
    public async Task<Result<ObservableCollection<ScheduledClassResponse>>> GetScheduledClasses()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("");
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
            ObservableCollection<ScheduledClassResponse>? scheduledClasses = JsonSerializer.Deserialize<ObservableCollection<ScheduledClassResponse>>(responseBody, jsonSerializerOptions);
            return Result<ObservableCollection<ScheduledClassResponse>>.Success(scheduledClasses) ?? Result<ObservableCollection<ScheduledClassResponse>>.Failure("Unexpected error during loading scheduled classes");
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
                    return Result<ObservableCollection<ScheduledClassResponse>>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ObservableCollection<ScheduledClassResponse>>.Failure($"Fatal error {ex.Message}");
            }

            return Result<ObservableCollection<ScheduledClassResponse>>.Failure(errorMessage);
        }
    }
}
