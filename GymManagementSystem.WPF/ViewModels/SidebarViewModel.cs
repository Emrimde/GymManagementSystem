using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ClassBooking;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using GymManagementSystem.WPF.ViewModels.Contract;
using GymManagementSystem.WPF.ViewModels.Employee;
using GymManagementSystem.WPF.ViewModels.EmploymentTemplate;
using GymManagementSystem.WPF.ViewModels.GymClass;
using GymManagementSystem.WPF.ViewModels.Membership;
using GymManagementSystem.WPF.ViewModels.ScheduledClass;
using GymManagementSystem.WPF.ViewModels.Settings;
using GymManagementSystem.WPF.ViewModels.Termination;
using GymManagementSystem.WPF.ViewModels.Trainer;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels;

public class SidebarViewModel : ViewModel
{
    public ICommand OpenMembershipView { get; }
    public ICommand OpenRegisterViewCommand { get; }
    public ICommand OpenClientView { get; }
    public ICommand OpenDashboardView { get; }
    public ICommand OpenContractView { get; }
    public ICommand OpenSettingsView { get; }
    public ICommand OpenTerminationView { get;  }
    public ICommand OpenClientMembershipView { get;  }
    public ICommand OpenTrainersView { get;  }
    public ICommand OpenGymClassesView { get;  }
    public ICommand OpenScheduledClassesView { get;  }
    public ICommand OpenClassBookingsView { get;  }
    public ICommand OpenEmployeesView { get; }
    public ICommand OpenEmploymentTemplatesViewCommand { get; }

    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }

    public SidebarViewModel(INavigationService navigationService)
    {
        _navigation = navigationService;
        OpenRegisterViewCommand = new RelayCommand(o => Navigation.NavigateTo<RegisterViewModel>(), o => true);
        OpenClientView = new RelayCommand(o => Navigation.NavigateTo<ClientViewModel>(), o => true);
        OpenDashboardView = new RelayCommand(item => Navigation.NavigateTo<DashboardViewModel>(), o => true);
        OpenMembershipView = new RelayCommand(item => Navigation.NavigateTo<MembershipViewModel>(), o => true);
        OpenContractView = new RelayCommand(item => Navigation.NavigateTo<ContractViewModel>(), o => true);
        OpenSettingsView = new RelayCommand(item => Navigation.NavigateTo<GeneralSettingsViewModel>(), o => true);
        OpenTerminationView = new RelayCommand(item => Navigation.NavigateTo<TerminationViewModel>(), item => true);
        OpenClientMembershipView = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipViewModel>(), item => true);
        OpenTrainersView = new RelayCommand(item => Navigation.NavigateTo<TrainerViewModel>(), item => true);
        OpenGymClassesView = new RelayCommand(item => Navigation.NavigateTo<GymClassViewModel>(), item => true);
        OpenScheduledClassesView = new RelayCommand(item => Navigation.NavigateTo<ScheduledClassViewModel>(), item => true);
        OpenClassBookingsView = new RelayCommand(item => Navigation.NavigateTo<ClassBookingViewModel>(), item => true);
        OpenEmployeesView = new RelayCommand(item => Navigation.NavigateTo<EmployeeViewModel>(), item => true);
        OpenEmploymentTemplatesViewCommand = new RelayCommand(item => Navigation.NavigateTo<EmploymentTemplateViewModel>(), item => true);
        
    }
}
