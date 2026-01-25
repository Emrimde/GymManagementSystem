using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.GymClass;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduledClass;
public class ScheduledClassDetailsViewModel : ViewModel, IParameterReceiver
{
    public INavigationService Navigation{ get; set;}
    public ICommand CancelScheduledClassCommand { get; }
    public ICommand LoadScheduledClassCommand { get; }

    private ScheduledClassDetailsResponse _scheduledClass = new();

    public ScheduledClassDetailsResponse ScheduledClass
    {
        get { return _scheduledClass; }
        set { _scheduledClass = value; OnPropertyChanged(); }
    }

    private readonly ScheduledClassHttpClient _scheduledHttpClient;

    public SidebarViewModel SidebarView { get; }

    public ScheduledClassDetailsViewModel(INavigationService navigation, ScheduledClassHttpClient scheduledHttpClient, SidebarViewModel sidebarView)
    {
        Navigation = navigation;
        _scheduledHttpClient = scheduledHttpClient;
        SidebarView = sidebarView;
        CancelScheduledClassCommand = new AsyncRelayCommand(item => CancelScheduledClassAsync(), item => true);
        LoadScheduledClassCommand = new AsyncRelayCommand(item => LoadScheduledClass(), item => true);
        
    }

    private async Task CancelScheduledClassAsync()
    {
        MessageBoxResult mbResult = MessageBox.Show("Are you sure to cancel schedule class?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (mbResult == MessageBoxResult.Yes)
        {
            Result<Unit> result = await _scheduledHttpClient.CancelScheduleClass(_scheduledClassId);
            if (!result.IsSuccess)
            {
                MessageBox.Show($"{result.ErrorMessage}");
            }
            Navigation.NavigateTo<ScheduledClassViewModel>(ScheduledClass.GymClassId);
        }
    }

    private Guid _scheduledClassId;

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _scheduledClassId = id;
        }
    }

    private async Task LoadScheduledClass()
    {
        Result<ScheduledClassDetailsResponse> result = await _scheduledHttpClient.GetScheduledClassById(_scheduledClassId);
        if (result.IsSuccess)
        {
            ScheduledClass = result.Value!;
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}");
        }
    }
}