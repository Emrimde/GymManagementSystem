using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;

public class ClientMembershipHttpClient : BaseHttpClientService
{
    public ClientMembershipHttpClient(HttpClient httpClient) : base(httpClient)
    {

    }

    public async Task<ObservableCollection<ClientMembershipResponse>> GetClientMembershipsAsync(Guid clientId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"membership-history/{clientId}");
        if (response.IsSuccessStatusCode)
        {
            ObservableCollection<ClientMembershipResponse>? clientMemberships = await response.Content.ReadFromJsonAsync<ObservableCollection<ClientMembershipResponse>>();

            return clientMemberships ?? new ObservableCollection<ClientMembershipResponse>();
        }
        else
        {
            MessageBox.Show("Failed to load", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return new ObservableCollection<ClientMembershipResponse>();
        }
    }

    public async Task<Result<ClientMembershipInfoResponse>> PostClientMembershipAsync(ClientMembershipAddRequest request)
    {
        request.StartDate = DateTime.SpecifyKind(request.StartDate, DateTimeKind.Utc);
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            ClientMembershipInfoResponse? clientMembership = JsonSerializer.Deserialize<ClientMembershipInfoResponse>(responseBody, options);
            return Result<ClientMembershipInfoResponse>.Success(clientMembership!);
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
                    return Result<ClientMembershipInfoResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ClientMembershipInfoResponse>.Failure($"Error {ex.Message}");
            }

            return Result<ClientMembershipInfoResponse>.Failure(errorMessage);
        }
    }
}
