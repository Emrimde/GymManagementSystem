using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.Core.Resulttttt;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class ClientHttpClient : BaseHttpClientService
{
    public ClientHttpClient(HttpClient httpClient) : base(httpClient)
    {
    }

    public Task<Result<ClientAgeValidationResponse>> ValidateClientAgeAsync(
        ClientAgeValidationRequest request)
    {
        return PostAsync<ClientAgeValidationRequest, ClientAgeValidationResponse>(
            "validate", request);
    }

    public Task<Result<ClientInfoResponse>> GetClientNameById(Guid clientId)
    {
        return GetAsync<ClientInfoResponse>($"name/{clientId}");
    }

    public Task<Result<PageResult<ClientResponse>>> GetAllClientsAsync(
     string? searchText, int page, bool? selectedIsActive)
    {
        var query = new List<string>
    {
        $"page={page}"
    };

        if (!string.IsNullOrWhiteSpace(searchText))
            query.Add($"searchText={Uri.EscapeDataString(searchText)}");

        if (selectedIsActive.HasValue)
            query.Add($"isActive={selectedIsActive.Value}");

        return GetAsync<PageResult<ClientResponse>>($"?{string.Join("&", query)}");
    }

    public Task<Result<ClientInfoResponse>> PostClientAsync(ClientAddRequest request)
    {
        request.DateOfBirth =
            DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Local).ToUniversalTime();

        return PostAsync<ClientAddRequest, ClientInfoResponse>("", request);
    }

    public Task<Result<ClientInfoResponse>> PutClientAsync(
        ClientUpdateRequest request, Guid id)
    {
        return PutAsync<ClientUpdateRequest, ClientInfoResponse>(
            $"{id}", request);
    }

    public Task<Result<ClientDetailsResponse>> GetClientById(Guid id)
    {
        return GetAsync<ClientDetailsResponse>($"{id}");
    }

    public Task<Result<ClientEditResponse>> GetClientForEditByClientIdAsync(Guid id)
    {
        return GetAsync<ClientEditResponse>($"get-for-edit/{id}");
    }
}
