using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Enum;
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

    //zwracam result bo umozliwia mi to  elastyczne zwracanie w przypadku sukcesu obiektu a w przeciwnym razie bledy z api
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
            catch (JsonException ex) { return Result<ClientResponse>.Failure(ex.Message, StatusCodeEnum.InternalServerError); } // obsluguje sytuacje wyjatku w parsowaniu

            return Result<ClientResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
        }
    }

    public async Task<Result<ClientResponse>> PutClientAsync(ClientUpdateRequest request, Guid id)
    {
        request.DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Utc); // był błąd z postgresql, strefy czasowe
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
            catch (JsonException ex) { return Result<ClientResponse>.Failure(ex.Message, StatusCodeEnum.InternalServerError); } // obsluguje sytuacje wyjatku w parsowaniu

            return Result<ClientResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
        }
    }

    public async Task<Result<ClientDetailsResponse>> GetClientById(Guid id, bool isActiveOnly = true)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{id}?isActiveOnly={isActiveOnly}");
        if (response.IsSuccessStatusCode)
        {
           ClientDetailsResponse? client = await response.Content.ReadFromJsonAsync<ClientDetailsResponse>();
           return Result<ClientDetailsResponse>.Success(client!) ?? Result<ClientDetailsResponse>.Failure("Client details not found");
        }
        else
        {
            return Result<ClientDetailsResponse>.Failure("Client details not found");
        }
    }
}
