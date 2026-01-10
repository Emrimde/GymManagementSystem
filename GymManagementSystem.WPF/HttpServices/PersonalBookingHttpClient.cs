using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.Result;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace GymManagementSystem.WPF.HttpServices;

public class PersonalBookingHttpClient : BaseHttpClientService
{
    public PersonalBookingHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
    public async Task<Result<PersonalBookingInfoResponse>> CreateAsync(PersonalBookingAddRequest dto)
    {
        var response = await _httpClient.PostAsJsonAsync("", dto);
        var body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<PersonalBookingInfoResponse>(body);
            return Result<PersonalBookingInfoResponse>.Success(result!);
        }

        return Result<PersonalBookingInfoResponse>.Failure(body);
    }

    public async Task<Result<bool>> DeleteAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"cancel/{id}");

        if (response.IsSuccessStatusCode)
            return Result<bool>.Success(true);

        var body = await response.Content.ReadAsStringAsync();
        return Result<bool>.Failure(body);
    }

    public async Task<Result<PersonalBookingInfoResponse>> GetPersonalBookingAsync(Guid id)
    {
        try
        {

            var response = await _httpClient.GetFromJsonAsync<PersonalBookingInfoResponse>($"{id}");
            return Result<PersonalBookingInfoResponse>.Success(response!);
        }

        catch (HttpRequestException ex)
        {
            return Result<PersonalBookingInfoResponse>.Failure($"{ex.Message}");
        }
    }

    public async Task<Result<PersonalBookingResponse>> UpdateAsync(Guid id, PersonalBookingUpdateRequest dto)
    {
        var response = await _httpClient.PutAsJsonAsync($"{id}", dto);
        var body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<PersonalBookingResponse>(body);
            return Result<PersonalBookingResponse>.Success(result!);
        }

        return Result<PersonalBookingResponse>.Failure(body);
    }

    public async Task<Result<PersonalBookingInfoResponse>> SetStatusToPaidAsync(Guid id)
    {
        var response = await _httpClient.PatchAsJsonAsync(
            $"pay-client/{id}",  
            new { }                     
        );

        var body = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<PersonalBookingInfoResponse>(body);
            return Result<PersonalBookingInfoResponse>.Success(result!);
        }

        return Result<PersonalBookingInfoResponse>.Failure(body);
    }

}

