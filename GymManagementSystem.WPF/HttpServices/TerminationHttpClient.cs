using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.WPF.Result;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class TerminationHttpClient : BaseHttpClientService
{
    public TerminationHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<TerminationResponse>> PostTerminationAsync(
        TerminationAddRequest request)
    {
        return PostAsync<TerminationAddRequest, TerminationResponse>(
            "",
            request
        );
    }
}
