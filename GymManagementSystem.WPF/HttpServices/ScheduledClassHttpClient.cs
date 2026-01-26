using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.WPF.Result;
using System.Collections.ObjectModel;
using System.Net.Http;

namespace GymManagementSystem.WPF.HttpServices;

public class ScheduledClassHttpClient : BaseHttpClientService
{
    public ScheduledClassHttpClient(HttpClient httpClient)
        : base(httpClient)
    {
    }

    public Task<Result<ObservableCollection<ScheduledClassResponse>>> GetScheduledClasses(
        string? searchText,
        Guid gymClassId)
    {
        string url = string.IsNullOrWhiteSpace(searchText)
            ? $"by-gymclass/{gymClassId}"
            : $"by-gymclass/{gymClassId}?searchText={Uri.EscapeDataString(searchText)}";

        return GetAsync<ObservableCollection<ScheduledClassResponse>>(url);
    }

    public Task<Result<ObservableCollection<ScheduledClassComboBoxResponse>>> GetScheduledClassesComboBox(
        Guid gymClassId,
        Guid membershipId,
        Guid clientId)
    {
        return GetAsync<ObservableCollection<ScheduledClassComboBoxResponse>>(
            $"scheduledclasses/{gymClassId}/{membershipId}/{clientId}"
        );
    }

    public Task<Result<ScheduledClassDetailsResponse>> GetScheduledClassById(Guid id)
    {
        return GetAsync<ScheduledClassDetailsResponse>($"{id}");
    }

    public Task<Result<Unit>> CancelScheduleClass(Guid scheduleClassId)
    {
        return DeleteAsync($"cancel-schedule-class/{scheduleClassId}");
    }
}
