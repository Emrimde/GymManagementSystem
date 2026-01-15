using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Auth;
using GymManagementSystem.WPF.ViewModels.ClassBooking;
using GymManagementSystem.WPF.ViewModels.Client;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using GymManagementSystem.WPF.ViewModels.Contract;
using GymManagementSystem.WPF.ViewModels.Dashboard;
using GymManagementSystem.WPF.ViewModels.Employee;
using GymManagementSystem.WPF.ViewModels.EmploymentTermination;
using GymManagementSystem.WPF.ViewModels.GymClass;
using GymManagementSystem.WPF.ViewModels.Membership;
using GymManagementSystem.WPF.ViewModels.ScheduledClass;
using GymManagementSystem.WPF.ViewModels.Settings;
using GymManagementSystem.WPF.ViewModels.Staff;
using GymManagementSystem.WPF.ViewModels.Termination;
using GymManagementSystem.WPF.ViewModels.Trainer;
using GymManagementSystem.WPF.ViewModels.TrainerContract;
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
    public ICommand OpenEmployeesView { get; }
    public ICommand OpenTrainerContractsView { get; }
    public ICommand OpenEmploymentTerminationsViewCommand { get; }
    public ICommand OpenStaffView { get; }

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
        OpenSettingsView = new RelayCommand(item => Navigation.NavigateTo<GeneralSettingsViewModel>(), o => true);
        OpenClientMembershipView = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipViewModel>(), item => true);
        OpenGymClassesView = new RelayCommand(item => Navigation.NavigateTo<GymClassViewModel>(), item => true);
        OpenEmploymentTerminationsViewCommand = new RelayCommand(item => Navigation.NavigateTo<EmploymentTerminationViewModel>(), item => true);
        OpenStaffView = new RelayCommand(item => Navigation.NavigateTo<StaffViewModel>(), item => true);
    }
}
