using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ClassBooking;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduledClass;
public class ScheduledClassDetailsViewModel : ViewModel, IParameterReceiver
{
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

    public ICommand OpenClassBookingAddView { get;  }

    private ScheduledClassDetailsResponse _scheduledClass;

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
        OpenClassBookingAddView = new RelayCommand(item => Navigation.NavigateTo<ClassBookingAddViewModel>(item), item => true);
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            _ = LoadScheduledClass(id);
        }
    }

    private async Task LoadScheduledClass(Guid id)
    {
        Result<ScheduledClassDetailsResponse> result = await _scheduledHttpClient.GetScheduledClassById(id);
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