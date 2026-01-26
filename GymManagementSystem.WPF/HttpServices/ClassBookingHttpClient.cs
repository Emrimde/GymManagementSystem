using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class ClassBookingHttpClient : BaseHttpClientService
{
    public ClassBookingHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<Unit>> DeleteClassBookingAsync(Guid classBookingId)
    {
        return DeleteAsync($"{classBookingId}");
    }

    public Task<Result<ClassBookingInfoResponse>> PostClassBookingAsync(
        ClassBookingAddRequest request)
    {
        return PostAsync<ClassBookingAddRequest, ClassBookingInfoResponse>(
            "",
            request
        );
    }

    public Task<Result<ObservableCollection<ClassBookingResponse>>> GetClassBookingsByClientId(
        Guid clientId)
    {
        return GetAsync<ObservableCollection<ClassBookingResponse>>(
            $"getAll/{clientId}"
        );
    }
}
