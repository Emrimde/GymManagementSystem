using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class EmploymentTerminationHttpClient : BaseHttpClientService
{
    public EmploymentTerminationHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<EmploymentTerminationGenerateResponse>> GetEmploymentTerminationDetailsAsync(
        Guid personId)
    {
        return GetAsync<EmploymentTerminationGenerateResponse>(
            $"{personId}"
        );
    }

    public Task<Result<EmploymentTerminationInfoResponse>> CreateEmploymentTerminationAsync(
        EmploymentTerminationAddRequest request)
    {
        return PostAsync<EmploymentTerminationAddRequest, EmploymentTerminationInfoResponse>(
            "",
            request
        );
    }

    public Task<Result<ObservableCollection<EmploymentTerminationResponse>>> GetAllEmploymentTerminationsAsync()
    {
        return GetAsync<ObservableCollection<EmploymentTerminationResponse>>("");
    }
}
