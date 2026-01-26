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
    public async Task<Result<Unit>> DeleteClassBookingAsync(Guid classBookingId)
    {
        HttpResponseMessage response =
            await _httpClient.DeleteAsync($"{classBookingId}");

        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return Result<Unit>.Success(Unit.Value);

        try
        {
            var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);

            if (errorDict != null)
            {
                if (errorDict.TryGetValue("detail", out var detail))
                    return Result<Unit>.Failure(detail.GetString() ?? "Unknown error.");

                if (errorDict.TryGetValue("errors", out var errorsElement))
                {
                    var errors = errorsElement.EnumerateObject()
                        .SelectMany(e => e.Value.EnumerateArray().Select(x => x.GetString()))
                        .Where(x => !string.IsNullOrEmpty(x));

                    return Result<Unit>.Failure(string.Join("\n", errors));
                }
            }
        }
        catch
        {

        }

        return Result<Unit>.Failure(response.ReasonPhrase ?? "Unknown error.");
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
        }

        return Result<ClassBookingInfoResponse>.Failure(response.ReasonPhrase ?? "Unknown error.");
    }
    public async Task<Result<ObservableCollection<ClassBookingResponse>>> GetClassBookingsByClientId(Guid clientId)
    {
        try
        {
            var classBookings = await _httpClient.GetFromJsonAsync<ObservableCollection<ClassBookingResponse>>(
                $"getAll/{clientId}");

            return Result<ObservableCollection<ClassBookingResponse>>.Success(
                classBookings);
        }
        catch (HttpRequestException ex)
        {
            return Result<ObservableCollection<ClassBookingResponse>>.Failure(ex.Message);
        }
    }
}
