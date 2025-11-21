using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;

public class ClientHttpClient : BaseHttpClientService
{
    public ClientHttpClient(HttpClient httpClient) : base(httpClient)
    {
        
    }

    public async Task<ObservableCollection<ClientResponse>> GetAllClientsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            ObservableCollection<ClientResponse>? clients = await response.Content.ReadFromJsonAsync<ObservableCollection<ClientResponse>>();

            return clients ?? new ObservableCollection<ClientResponse>();
        }
        else 
        {
            MessageBox.Show("Failed to load clients.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return new ObservableCollection<ClientResponse>();
        }
    }

    //zwracam result bo umozliwia mi to  elastyczne zwracanie w   przypadku sukcesu obiektu a w przeciwnym razie bledy z api
    public async Task<Result<ClientResponse>> PostClientAsync(ClientAddRequest request)
    {
        request.DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth,DateTimeKind.Utc); // był błąd z postgresql, strefy czasowe
        string json = JsonSerializer.Serialize(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync((string?)null, content);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions // backend zwracał camelCase
            {
                PropertyNameCaseInsensitive = true
            };
            ClientResponse? client = JsonSerializer.Deserialize<ClientResponse>(responseBody,options);
            return Result<ClientResponse>.Success(client!);
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
                    return Result<ClientResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ClientResponse>.Failure($"Fatal error {ex.Message}");
            }

            return Result<ClientResponse>.Failure(errorMessage);
        }
    }

    public async Task<Result<ClientResponse>> PutClientAsync(ClientUpdateRequest request, Guid id)
    {
        request.DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Utc);
        string json = JsonSerializer.Serialize(request);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync($"{id}", content);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions // backend zwracał camelCase
            {
                PropertyNameCaseInsensitive = true
            };
            ClientResponse? client = JsonSerializer.Deserialize<ClientResponse>(responseBody, options);
            return Result<ClientResponse>.Success(client!);
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
                    return Result<ClientResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ClientResponse>.Failure($"Fatal error {ex.Message}");
            }

            return Result<ClientResponse>.Failure(errorMessage);
        }
    }

    public async Task<Result<ClientDetailsResponse>> GetClientById(Guid id, bool isActiveOnly = true)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{id}?isActiveOnly={isActiveOnly}");
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
           ClientDetailsResponse? client = await response.Content.ReadFromJsonAsync<ClientDetailsResponse>();
           return Result<ClientDetailsResponse>.Success(client!) ?? Result<ClientDetailsResponse>.Failure("Client details not found");
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
                    return Result<ClientDetailsResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ClientDetailsResponse>.Failure($"Fatal rror {ex.Message}");
            }

            return Result<ClientDetailsResponse>.Failure(errorMessage);
        }
    }
}
