using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;

public class MembershipHttpClient : BaseHttpClientService
{
    public MembershipHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ObservableCollection<MembershipResponse>> GetAllMembershipsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            ObservableCollection<MembershipResponse>? memberships = await response.Content.ReadFromJsonAsync<ObservableCollection<MembershipResponse>>();

            return memberships ?? new ObservableCollection<MembershipResponse>();
        }
        else
        {
            MessageBox.Show("Failed to load clients.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return new ObservableCollection<MembershipResponse>();
        }
    }

    public async Task<Result<MembershipResponse>> PostMembershipAsync(MembershipAddRequest membershipAddRequest)
    {
        string json = JsonSerializer.Serialize(membershipAddRequest);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync((string?)null, content);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions // backend zwracał camelCase
            {
                PropertyNameCaseInsensitive = true
            };
            MembershipResponse? membership = JsonSerializer.Deserialize<MembershipResponse>(responseBody, options);
            return Result<MembershipResponse>.Success(membership!);
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
                    return Result<MembershipResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<MembershipResponse>.Failure($"Fatal error: {ex.Message}");
            }

            return Result<MembershipResponse>.Failure(errorMessage);
        }
    }

    public async Task<Result<MembershipResponse>> PutMembershipAsync(MembershipUpdateRequest membershipUpdateRequest, Guid membershipId)
    {
        string json = JsonSerializer.Serialize(membershipUpdateRequest);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync($"{membershipId}", content);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions // backend zwracał camelCase
            {
                PropertyNameCaseInsensitive = true
            };
            MembershipResponse? membership = JsonSerializer.Deserialize<MembershipResponse>(responseBody, options);
            return Result<MembershipResponse>.Success(membership!);
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
                    return Result<MembershipResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<MembershipResponse>.Failure($"Fatal error: {ex.Message}");
            }

            return Result<MembershipResponse>.Failure(errorMessage);
        }
    }
}
