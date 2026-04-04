using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduledClass;
public class ScheduledClassViewModel : ViewModel, IParameterReceiver
{
    private readonly ScheduledClassHttpClient _scheduledClassHttpClient;
    private readonly GymClassHtppClient _gymClassHttpCLient;
    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation { get; set; }
    private Guid _gymClassId { get; set; }
    private ObservableCollection<ScheduledClassResponse> _scheduledClasses = new();

    public ObservableCollection<ScheduledClassResponse> ScheduledClasses
    {
        get { return _scheduledClasses; }
        set { _scheduledClasses = value; OnPropertyChanged(); }
    }
    public ICommand OpenScheduledClassDetails { get; }
    public ICommand GenerateScheduledClass { get; }
    public ICommand LoadScheduleClassesCommand { get; }

    public ScheduledClassViewModel(ScheduledClassHttpClient scheduledClassHttpClient, SidebarViewModel sidebarView, INavigationService navigation,GymClassHtppClient gymClassHttpClient)
    {
        _scheduledClassHttpClient = scheduledClassHttpClient;
        _gymClassHttpCLient = gymClassHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        OpenScheduledClassDetails = new RelayCommand(item => Navigation.NavigateTo<ScheduledClassDetailsViewModel>(item!), item => true);
        LoadScheduleClassesCommand = new AsyncRelayCommand(item => LoadScheduledClasses(_gymClassId), item => true);
    }

    private async Task LoadScheduledClasses(Guid gymClassId)
    {
        Result<ObservableCollection<ScheduledClassResponse>> result = await _scheduledClassHttpClient.GetScheduledClasses(gymClassId);
        if (result.IsSuccess)
        {
            ScheduledClasses = result.Value!;
        }
        else
        {
            MessageBox.Show(result.GetUserMessage());
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid gymClassId)
        {
            _gymClassId = gymClassId;
        }
    }
}
