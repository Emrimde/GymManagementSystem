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
        Guid gymClassId)
    {
        string url = $"by-gymclass/{gymClassId}";
        return GetAsync<ObservableCollection<ScheduledClassResponse>>(url);
    }

    public Task<Result<ObservableCollection<ScheduledClassComboBoxResponse>>> GetScheduledClassesComboBox(
        Guid gymClassId,
        Guid clientId)
    {
        return GetAsync<ObservableCollection<ScheduledClassComboBoxResponse>>(
            $"scheduledclasses/{gymClassId}?clientId={clientId}"
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
