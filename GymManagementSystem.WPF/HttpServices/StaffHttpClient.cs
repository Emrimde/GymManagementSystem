using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ViewModels.Staff.Models;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class StaffHttpClient : BaseHttpClientService
{
    public StaffHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<ObservableCollection<PersonResponse>>> GetAllStaffAsync(string? searchText, StaffFilterRequest? staffFilterRequest, bool? selectedIsActive)
    {
        var query = new List<string> { $"searchText={Uri.EscapeDataString(searchText ?? string.Empty)}" };
        if (staffFilterRequest != null)
        {
            if (staffFilterRequest.IsTrainer.HasValue)
                query.Add($"isTrainer={staffFilterRequest.IsTrainer.Value}");

            if (staffFilterRequest.EmployeeRole.HasValue)
                query.Add($"employeeRole={staffFilterRequest.EmployeeRole.Value}");

            if (staffFilterRequest.TrainerType.HasValue)
                query.Add($"trainerType={staffFilterRequest.TrainerType.Value}");
        }
        if (selectedIsActive.HasValue)
            query.Add($"isActive={selectedIsActive.Value}");

        return GetAsync<ObservableCollection<PersonResponse>>($"?{string.Join("&", query)}");

    }


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
