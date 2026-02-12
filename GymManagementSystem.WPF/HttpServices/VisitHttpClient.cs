using GymManagementSystem.Core.DTO;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class VisitHttpClient : BaseHttpClientService
{
    public VisitHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<Unit>> RegisterVisitAsync(Guid clientId)
    {
        return PostAsync<object?, Unit>(
            $"register-visit/{clientId}",
            null
        );
    }

    public Task<Result<ObservableCollection<VisitResponse>>> GetAllClientVisitsAsync(
        Guid clientId)
    {
        return GetAsync<ObservableCollection<VisitResponse>>(
            $"{clientId}"
        );
    }
    public Task<Result<Unit>> DeleteVisitAsync(
        Guid visitId)
    {
        return DeleteAsync(
            $"{visitId}"
        );
    }
}
