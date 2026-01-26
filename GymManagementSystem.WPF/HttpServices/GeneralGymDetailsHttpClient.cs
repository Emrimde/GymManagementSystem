using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.WPF.Result;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagementSystem.WPF.HttpServices;

public class GeneralGymDetailsHttpClient : BaseHttpClientService
{
    public GeneralGymDetailsHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<GeneralGymResponse>> GetGeneralGymSettingsAsync()
    {
        return GetAsync<GeneralGymResponse>("");
    }

    public Task<Result<GeneralGymResponse>> PutGeneralSettingsAsync(
        GeneralGymUpdateRequest request)
    {
        return PutAsync<GeneralGymUpdateRequest, GeneralGymResponse>(
            "",
            request
        );
    }

    // multipart/form-data – świadomie poza BaseHttpClientService
    public async Task<Result<string>> UploadLogoAsync(
        MultipartFormDataContent content)
    {
        var response = await _httpClient.PostAsync("logo", content);
        return await HandleMultipartResponse(response);
    }

    // lokalna, minimalna obsługa tylko dla multipart
    private static async Task<Result<string>> HandleMultipartResponse(
        HttpResponseMessage response)
    {
        var url = await response.Content.ReadFromJsonAsync<string>();
        return Result<string>.Success(url!);
    }
}
