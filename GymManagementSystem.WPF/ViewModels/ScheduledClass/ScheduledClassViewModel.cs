using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduledClass;
public class ScheduledClassViewModel : ViewModel
{
    private readonly ScheduledClassHttpClient _scheduledClassHttpClient;
    public SidebarViewModel SidebarView { get; set; }
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<ScheduledClassResponse> _scheduledClasses;

    public ObservableCollection<ScheduledClassResponse> ScheduledClasses
    {
        get { return _scheduledClasses; }
        set { _scheduledClasses = value; OnPropertyChanged(); }
    }
    public ICommand OpenScheduledClassDetails { get; }

    public ScheduledClassViewModel(ScheduledClassHttpClient scheduledClassHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        ScheduledClasses = new ObservableCollection<ScheduledClassResponse>();
        _scheduledClassHttpClient = scheduledClassHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        OpenScheduledClassDetails = new RelayCommand(item => Navigation.NavigateTo<ScheduledClassDetailsViewModel>(item), item => true);
        _ = LoadScheduledClasses();
    }

    private async Task LoadScheduledClasses()
    {
        Result<ObservableCollection<ScheduledClassResponse>> result = await _scheduledClassHttpClient.GetScheduledClasses();
        if (result.IsSuccess)
        {
            ScheduledClasses = result.Value!;
        }
        else
        {
            MessageBox.Show(result.ErrorMessage);
        }
    }
}
