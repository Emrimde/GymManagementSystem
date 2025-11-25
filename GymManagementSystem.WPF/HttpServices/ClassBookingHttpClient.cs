using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;
public class ClassBookingHttpClient : BaseHttpClientService
{
    public ClassBookingHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<ClassBookingInfoResponse>> PostClassBookingAsync(ClassBookingAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);

        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var booking = await response.Content.ReadFromJsonAsync<ClassBookingInfoResponse>();
            if (booking == null)
                return Result<ClassBookingInfoResponse>.Failure("Empty response from server.");

            return Result<ClassBookingInfoResponse>.Success(booking);
        }

       
        try
        {
            var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);

            if (errorDict != null)
            {
                if (errorDict.TryGetValue("detail", out var detail))
                    return Result<ClassBookingInfoResponse>.Failure(detail.GetString() ?? "Unknown error.");

                // Obsługa błędów walidacji
                if (errorDict.TryGetValue("errors", out var errorsElement))
                {
                    var errors = errorsElement.EnumerateObject()
                        .SelectMany(e => e.Value.EnumerateArray().Select(item => item.GetString()))
                        .Where(item => !string.IsNullOrEmpty(item));

                    return Result<ClassBookingInfoResponse>.Failure(string.Join("\n", errors));
                }
            }
        }
        catch
        {
            // ignorujemy parsing error
        }

        return Result<ClassBookingInfoResponse>.Failure(response.ReasonPhrase ?? "Unknown error.");
    }
    public async Task<Result<ObservableCollection<ClassBookingResponse>>> GetClassBookings()
    {
        try
        {
            var classBookings = await _httpClient.GetFromJsonAsync<ObservableCollection<ClassBookingResponse>>(
                "");

            return Result<ObservableCollection<ClassBookingResponse>>.Success(
                classBookings);
        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<ClassBookingResponse>>.Failure(ex.Message);
        }
    }
}
