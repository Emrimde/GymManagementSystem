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

    private string _searchText;

    public string SearchText
    {
        get { return _searchText; }
        set { _searchText = value; OnPropertyChanged(); }
    }

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
    public ICommand SearchScheduledClassesCommand { get; }
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
        SearchScheduledClassesCommand = new AsyncRelayCommand(item => SearchScheduledClasses(), item => true);
    }

    private async Task SearchScheduledClasses()
    {
        Result<ObservableCollection<ScheduledClassResponse>> result = await _scheduledClassHttpClient.GetScheduledClasses(SearchText);
        if (result.IsSuccess)
        {
            ScheduledClasses = result.Value!;
        }
        else
        {
            MessageBox.Show(result.ErrorMessage);
        }
    }

    private async Task LoadScheduledClasses()
    {
        Result<ObservableCollection<ScheduledClassResponse>> result = await _scheduledClassHttpClient.GetScheduledClasses(null);
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
