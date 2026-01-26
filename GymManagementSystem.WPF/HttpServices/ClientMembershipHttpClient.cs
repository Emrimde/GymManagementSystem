using System.Collections.ObjectModel;
using System.Net.Http;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.WPF.Result;

namespace GymManagementSystem.WPF.HttpServices;

public class ClientMembershipHttpClient : BaseHttpClientService
{
    public ClientMembershipHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<ObservableCollection<ClientMembershipResponse>>> GetClientMembershipsHistoryAsync(
        Guid clientId)
    {
        return GetAsync<ObservableCollection<ClientMembershipResponse>>(
            $"membership-history/{clientId}"
        );
    }

    public Task<Result<ClientMembershipDetailsResponse>> GetClientMembershipDetailsAsync(
        Guid clientMembershipId)
    {
        return GetAsync<ClientMembershipDetailsResponse>(
            $"{clientMembershipId}"
        );
    }

    public Task<Result<ClientMembershipContractPreviewResponse>> GetContractPreviewDetailsAsync(
        Guid clientId,
        Guid membershipId)
    {
        return GetAsync<ClientMembershipContractPreviewResponse>(
            $"contract-preview/{clientId}/{membershipId}"
        );
    }

    public Task<Result<ClientMembershipInfoResponse>> PostClientMembershipAsync(
        ClientMembershipAddRequest request)
    {
        return PostAsync<ClientMembershipAddRequest, ClientMembershipInfoResponse>(
            "",
            request
        );
    }
}
