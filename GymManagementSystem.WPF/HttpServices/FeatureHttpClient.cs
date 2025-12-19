using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;
public class FeatureHttpClient : BaseHttpClientService
{
    public FeatureHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<ObservableCollection<FeatureResponse>>> GetFeaturesForSelect()
    {
        try
        {
            ObservableCollection<FeatureResponse>? result = await _httpClient.GetFromJsonAsync<ObservableCollection<FeatureResponse>>("");
            return Result<ObservableCollection<FeatureResponse>>.Success(result ?? new ObservableCollection<FeatureResponse>());
        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<FeatureResponse>>.Failure($"{ex.Message}");
        }
    }
    public async Task<Result<Unit>> PostFeatureAsync(FeatureAddRequest featureAddRequest)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", featureAddRequest);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return Result<Unit>.Success(new Unit());
        }

        Dictionary<string, JsonElement>? errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
        if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
        {
            string errorMessage = detailElement.ToString();
            return Result<Unit>.Failure(errorMessage);
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
            return Result<Unit>.Failure(string.Join("\n", allErrors));
        }

        return Result<Unit>.Failure("Something went wrong.");
    }

    
    }
}
