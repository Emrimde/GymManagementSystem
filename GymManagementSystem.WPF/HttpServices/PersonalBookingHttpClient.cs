using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.WPF.Result;
using System.Net.Http;
using System.Net.Http.Json;

namespace GymManagementSystem.WPF.HttpServices;

public class PersonalBookingHttpClient : BaseHttpClientService
{
    public PersonalBookingHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<PersonalBookingInfoResponse>> CreateAsync(
        PersonalBookingAddRequest request)
    {
        return PostAsync<PersonalBookingAddRequest, PersonalBookingInfoResponse>(
            "",
            request
        );
    }

    public Task<Result<Unit>> DeleteAsync(Guid id)
    {
        // backend używa GET do cancel → mapujemy, ale semantycznie to delete
        return GetAsync<Unit>($"cancel/{id}");
    }

    public Task<Result<PersonalBookingInfoResponse>> GetPersonalBookingAsync(
        Guid id)
    {
        return GetAsync<PersonalBookingInfoResponse>(
            $"{id}"
        );
    }

    public Task<Result<PersonalBookingResponse>> UpdateAsync(
        Guid id,
        PersonalBookingUpdateRequest request)
    {
        return PutAsync<PersonalBookingUpdateRequest, PersonalBookingResponse>(
            $"{id}",
            request
        );
    }

    public async Task<Result<PersonalBookingInfoResponse>> SetStatusToPaidAsync(Guid id)
    {
        return await PutAsync<PersonalBookingInfoResponse>($"pay-client/{id}");
    }
}
