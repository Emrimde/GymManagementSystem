using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
    public Task<Result<PersonalBookingForEditResponse>> GetPersonalBookingForEdit(
        Guid personalBookingId)
    {
        return GetAsync<PersonalBookingForEditResponse>(
            $"personal-booking-for-edit/{personalBookingId}"
        );
    }

    public Task<Result<PersonalBookingInfoResponse>> UpdateAsync(
        Guid id,
        PersonalBookingUpdateRequest request)
    {
        return PutAsync<PersonalBookingUpdateRequest, PersonalBookingInfoResponse>(
            $"{id}",
            request
        );
    }

    public async Task<Result<PersonalBookingInfoResponse>> SetStatusToPaidAsync(Guid id)
    {
        return await PutAsync<PersonalBookingInfoResponse>($"pay-client/{id}");
    }

    public Task<Result<ObservableCollection<PersonalBookingResponse>>> GetPersonalBookingsAsync(Guid clientId)
    {
        return GetAsync<ObservableCollection<PersonalBookingResponse>>(
           $"client-personal-bookings/{clientId}"
       );
    }

    public Task<Result<Unit>> DeletePersonalBookingAsync(Guid personalBookingId)
    {
        return DeleteAsync($"{personalBookingId}");
    }
}
