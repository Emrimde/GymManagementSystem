using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Enum;
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
                using var doc = JsonDocument.Parse(responseBody);

                if (doc.RootElement.TryGetProperty("errors", out JsonElement errorsElement)) // w errors są wszystkie błędy a nie jak się wydaje że w detail - detail nie istnieje
                {
                    var messages = new List<string>();
                    foreach (var prop in errorsElement.EnumerateObject())
                    {
                        foreach (var msg in prop.Value.EnumerateArray())
                        {
                            messages.Add($"{prop.Name}: {msg.GetString()}");
                        }
                    }
                    errorMessage = string.Join(Environment.NewLine, messages);
                }


            }
            catch (JsonException ex) { return Result<MembershipResponse>.Failure(ex.Message, StatusCodeEnum.InternalServerError); } // obsluguje sytuacje wyjatku w parsowaniu

            return Result<MembershipResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
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
                using var doc = JsonDocument.Parse(responseBody);

                if (doc.RootElement.TryGetProperty("errors", out JsonElement errorsElement)) // w errors są wszystkie błędy a nie jak się wydaje że w detail - detail nie istnieje
                {
                    var messages = new List<string>();
                    foreach (var prop in errorsElement.EnumerateObject())
                    {
                        foreach (var msg in prop.Value.EnumerateArray())
                        {
                            messages.Add($"{prop.Name}: {msg.GetString()}");
                        }
                    }
                    errorMessage = string.Join(Environment.NewLine, messages);
                }


            }
            catch (JsonException ex) { return Result<MembershipResponse>.Failure(ex.Message, StatusCodeEnum.InternalServerError); } // obsluguje sytuacje wyjatku w parsowaniu

            return Result<MembershipResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
        }
    }
}
