using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;
public class ClassBookingHttpClient : BaseHttpClientService
{
    public ClassBookingHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<ClassBookingInfoResponse>> PostClientAsync(ClassBookingAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions 
            {
                PropertyNameCaseInsensitive = true
            };
            ClassBookingInfoResponse? classBooking = JsonSerializer.Deserialize<ClassBookingInfoResponse>(responseBody, options);
            return Result<ClassBookingInfoResponse>.Success(classBooking!);
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
                    return Result<ClassBookingInfoResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ClassBookingInfoResponse>.Failure($"Fatal error {ex.Message}");
            }

            return Result<ClassBookingInfoResponse>.Failure(errorMessage);
        }
    }
}
