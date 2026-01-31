using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class GymClassHtppClient : BaseHttpClientService
{
    public GymClassHtppClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<ObservableCollection<GymClassResponse>>> GetGymClasses(bool? selectedStatusFilter)
    {
        string query = selectedStatusFilter.HasValue ? $"?isActive={selectedStatusFilter}" : "";
        return GetAsync<ObservableCollection<GymClassResponse>>(query);
    }

    public Task<Result<ObservableCollection<GymClassComboBoxResponse>>> GetGymClassComboBoxResponses()
    {
        return GetAsync<ObservableCollection<GymClassComboBoxResponse>>("select-gymclasses");
    }

    public Task<Result<GymClassForEditResponse>> GetGymClassForEdit(Guid gymClassId)
    {
        return GetAsync<GymClassForEditResponse>(
            $"get-gymclass-for-edit/{gymClassId}"
        );
    }

    public Task<Result<GymClassInfoResponse>> PostGymClassAsync(
        GymClassAddRequest request)
    {
        return PostAsync<GymClassAddRequest, GymClassInfoResponse>(
            "",
            request
        );
    }

    public Task<Result<Unit>> GenerateNewScheduledClasses(Guid gymClassId)
    {
        return PostAsync<object?, Unit>(
            $"{gymClassId}",
            null
        );
    }

    public Task<Result<Unit>> PutGymClassAsync(
        GymClassUpdateRequest gymClassUpdateRequest)
    {
        return PutAsync<GymClassUpdateRequest, Unit>(
            "",
            gymClassUpdateRequest
        );
    }

    public Task<Result<Unit>> DeleteGymClassAsync(Guid gymClassId)
    {
        return DeleteAsync($"{gymClassId}");
    }

    public  Task<Result<Unit>> RestoreGymClassAsync(Guid gymClassId)
    {
        return PutAsync<Unit>($"restore/{gymClassId}");
    }
}
