using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;
public class MembershipPriceHttpClient : BaseHttpClientService
{
    public MembershipPriceHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<Result<Unit>> PostMembershipPriceAsync(MembershipPriceAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return Result<Unit>.Success(new Unit());
        }

        Dictionary<string, JsonElement>? errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
        if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
        {
            string errorMessage = detailElement.ToString();
            return Result<Unit>.Failure(errorMessage);
        }
        if (errorDict != null && errorDict.TryGetValue("errors", out var errorsElement))
        {
            List<string> allErrors = new List<string>();
            foreach (var errorProp in errorsElement.EnumerateObject())
            {
                var messages = errorProp.Value.EnumerateArray();
                foreach (var item in messages)
                {
                    string msg = item.ToString();
                    allErrors.Add(msg);
                }
            }
            return Result<Unit>.Failure(string.Join("\n", allErrors));
        }

        return Result<Unit>.Failure("Something went wrong.");
    }


    public async Task<ObservableCollection<MembershipPriceResponse>> GetAllMembershipsPricesAsync(Guid membershipId)
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
