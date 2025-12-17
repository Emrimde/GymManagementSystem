using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.DTO.MembershipPrice;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;
public class MembershipPriceHttpClient : BaseHttpClientService
{
    public MembershipPriceHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }


    public async Task<ObservableCollection<MembershipPriceResponse>> GetAllMembershipsAsync(Guid membershipId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{membershipId}");
        if (response.IsSuccessStatusCode)
        {
            ObservableCollection<MembershipPriceResponse>? memberships = await response.Content.ReadFromJsonAsync<ObservableCollection<MembershipPriceResponse>>();

            return memberships ?? new ObservableCollection<MembershipPriceResponse>();
        }
        else
        {
            MessageBox.Show("Failed to load clients.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return new ObservableCollection<MembershipPriceResponse>();
        }
    }
}
