using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class MembershipPriceHttpClient : BaseHttpClientService
{
    public MembershipPriceHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<Unit>> PostMembershipPriceAsync(
        MembershipPriceAddRequest request)
    {
        return PostAsync<MembershipPriceAddRequest, Unit>(
            "",
            request
        );
    }

    public Task<Result<ObservableCollection<MembershipPriceResponse>>> GetAllMembershipsPricesAsync(
        Guid membershipId)
    {
        return GetAsync<ObservableCollection<MembershipPriceResponse>>(
            $"{membershipId}"
        );
    }
}
