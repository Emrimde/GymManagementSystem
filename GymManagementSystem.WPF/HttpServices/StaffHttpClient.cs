using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class StaffHttpClient : BaseHttpClientService
{
    public StaffHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<ObservableCollection<PersonResponse>>> GetAllStaffAsync(string? searchText)
       => GetAsync<ObservableCollection<PersonResponse>>(
        $"?searchText={Uri.EscapeDataString(searchText ?? string.Empty)}"
    );
        
    public Task<Result<PersonDetailsResponse>> GetPersonDetailsAsync(
        Guid personId)
        => GetAsync<PersonDetailsResponse>($"{personId}");

    public Task<Result<Unit>> PostPersonToStaffAsync(
        PersonAddRequest request)
        => PostAsync<PersonAddRequest, Unit>("", request);

    public Task<Result<Unit>> PutPersonToStaffAsync(
        PersonUpdateRequest request)
        => PutAsync<PersonUpdateRequest, Unit>("", request);

    public Task<Result<PersonForEditResponse>> GetPersonForEditAsync(
        Guid personId)
        => GetAsync<PersonForEditResponse>(
            $"get-person-for-edit/{personId}"
        );
}
