using GymManagementSystem.Core.DTO.Termination;
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
            string errorMessage = responseBody;

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                     errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<TerminationResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<TerminationResponse>.Failure($"Fatal error: {ex.Message}");
            }

            return Result<TerminationResponse>.Failure(errorMessage);
        }
    }
}
