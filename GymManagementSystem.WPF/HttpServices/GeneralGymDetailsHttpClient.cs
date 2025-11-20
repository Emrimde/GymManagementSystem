using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;

public class GeneralGymDetailsHttpClient : BaseHttpClientService
{
    public GeneralGymDetailsHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
    public async Task<GeneralGymUpdateRequest> GetGeneralGymSettingsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            GeneralGymUpdateRequest? generalGymResponse = await response.Content.ReadFromJsonAsync<GeneralGymUpdateRequest>();

            return generalGymResponse ?? new GeneralGymUpdateRequest();
        }
        else
        {
            MessageBox.Show("Failed to load Gym setting.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return new GeneralGymUpdateRequest();
        }
    }

    public async Task<Result<GeneralGymResponse>> PutGeneralSettingsAsync(GeneralGymUpdateRequest request)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync("", request);
        if (response.IsSuccessStatusCode)
        {
            GeneralGymResponse? generalGymResponse = await response.Content.ReadFromJsonAsync<GeneralGymResponse>();

            return Result<GeneralGymResponse>.Success(generalGymResponse);
        }
        else
        {
            return Result<GeneralGymResponse>.Failure("Failed to update settings.", StatusCodeEnum.InternalServerError);
        }
    }
}
