using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;

public class TerminationHttpClient : BaseHttpClientService
{
    public TerminationHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public async Task<ObservableCollection<TerminationResponse>> GetTerminationsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            ObservableCollection<TerminationResponse>? terminations = await response.Content.ReadFromJsonAsync<ObservableCollection<TerminationResponse>>();

            return terminations ?? new ObservableCollection<TerminationResponse>();
        }
        else
        {
            MessageBox.Show("Failed to load", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return new ObservableCollection<TerminationResponse>();
        }
    }

    public async Task<Result<TerminationResponse>> PostTerminationAsync(TerminationAddRequest request)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync("", request);
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            TerminationResponse? termination = JsonSerializer.Deserialize<TerminationResponse>(responseBody, options);
            return Result<TerminationResponse>.Success(termination!);
        }
        else
        {
            string errorMessage = responseBody; // fallback na cały responseBody

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                     errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<TerminationResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
                }
            }
            catch (JsonException)
            {
                // jeśli nie uda się zdeserializować JSON, zostaje cały responseBody
            }

            return Result<TerminationResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
        }
    }

    public async Task<Result<bool>> CanCreateTerminationAsync(Guid clientId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{clientId}/can-create-termination");

        if (response.IsSuccessStatusCode)
        {
            return Result<bool>.Success(true);
        }
        else
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            string errorMessage = responseBody; // fallback na cały responseBody

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<bool>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
                }
            }
            catch (JsonException)
            {
                // jeśli nie uda się zdeserializować JSON, zostaje cały responseBody
            }

            return Result<bool>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
        }
    }
}
