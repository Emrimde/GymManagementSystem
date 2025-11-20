using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;

public class ContractHttpClient : BaseHttpClientService
{
    public ContractHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }
    public async Task<ObservableCollection<ContractResponse>> GetContractsAsync()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            ObservableCollection<ContractResponse>? contracts = await response.Content.ReadFromJsonAsync<ObservableCollection<ContractResponse>>();

            return contracts ?? new ObservableCollection<ContractResponse>();
        }
        else
        {
            MessageBox.Show("Failed to load clients.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return new ObservableCollection<ContractResponse>();
        }
    }

    public async Task<Result<ContractResponse>> PutContractAsync(ContractUpdateRequest request, Guid id)
    {
        HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{id}", request);

        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var options = new JsonSerializerOptions // backend zwracał camelCase
            {
                PropertyNameCaseInsensitive = true
            };
            ContractResponse? client = JsonSerializer.Deserialize<ContractResponse>(responseBody, options);
            return Result<ContractResponse>.Success(client!);
        }
        else
        {
            string errorMessage = responseBody;
            try
            {
                using var doc = JsonDocument.Parse(responseBody);

                if (doc.RootElement.TryGetProperty("errors", out JsonElement errorsElement)) // w errors są wszystkie błędy a nie jak się wydaje że w detail - detail 
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
            catch (JsonException ex) { return Result<ContractResponse>.Failure(ex.Message, StatusCodeEnum.InternalServerError); } // obsluguje sytuacje wyjatku w parsowaniu

            return Result<ContractResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
        }
    }

    public async Task<Result<ContractDetailsResponse>> GetContractByIdAsync(Guid id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{id}");
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {   
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };    
            ContractDetailsResponse? contractDetailsResponse = JsonSerializer.Deserialize<ContractDetailsResponse>(responseBody, options);
            return Result<ContractDetailsResponse>.Success(contractDetailsResponse!);
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
                    return Result<ContractDetailsResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
                }
            }
            catch (JsonException)
            {
                // jeśli nie uda się zdeserializować JSON, zostaje cały responseBody
            }

            return Result<ContractDetailsResponse>.Failure(errorMessage, StatusCodeEnum.InternalServerError);
        }
    }
}
