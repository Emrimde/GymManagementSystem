using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.Result;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;

namespace GymManagementSystem.WPF.HttpServices;

public class ContractHttpClient : BaseHttpClientService
{
    public ContractHttpClient(HttpClient httpClient) : base(httpClient)
    {
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
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<ContractResponse>.Failure(errorMessage);
                }
            }

            catch (Exception ex) 
            {
                return Result<ContractResponse>.Failure($"Error {ex.Message}");
            } 

            return Result<ContractResponse>.Failure(errorMessage);
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
            string errorMessage = responseBody;

            try
            {
                var errorDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(responseBody);
                if (errorDict != null && errorDict.TryGetValue("detail", out var detailElement))
                {
                    errorMessage = detailElement.GetString() ?? responseBody;
                    return Result<ContractDetailsResponse>.Failure(errorMessage);
                }
            }
            catch (Exception ex)
            {
                return Result<ContractDetailsResponse>.Failure($"{ex.Message}");
            }

            return Result<ContractDetailsResponse>.Failure(errorMessage);
        }
    }
}
