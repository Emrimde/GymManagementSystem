using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagementSystem.WPF.HttpServices;
public class FeatureHttpClient : BaseHttpClientService
{
    public FeatureHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<ObservableCollection<FeatureSelectResponse>>> GetFeaturesForSelect()
    {
        try
        {
            ObservableCollection<FeatureSelectResponse>? result = await _httpClient.GetFromJsonAsync<ObservableCollection<FeatureSelectResponse>>("");
            return Result<ObservableCollection<FeatureSelectResponse>>.Success(result ?? new ObservableCollection<FeatureSelectResponse>());
        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<FeatureSelectResponse>>.Failure($"{ex.Message}");
        }
    }
}
