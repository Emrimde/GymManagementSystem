using GymManagementSystem.WPF.Services;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Auth;
using GymManagementSystem.WPF.ViewModels.Client;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using GymManagementSystem.WPF.ViewModels.Dashboard;
using GymManagementSystem.WPF.ViewModels.EmploymentTermination;
using GymManagementSystem.WPF.ViewModels.GymClass;
using GymManagementSystem.WPF.ViewModels.Membership;
using GymManagementSystem.WPF.ViewModels.Settings;
using GymManagementSystem.WPF.ViewModels.Staff;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels;

public class SidebarViewModel : ViewModel
{
    private readonly AuthService _authService;
    public ICommand OpenMembershipView { get; }
    public ICommand OpenRegisterViewCommand { get; }
    public ICommand OpenClientView { get; }
    public ICommand OpenDashboardView { get; }
    public ICommand OpenSettingsView { get; }
    public ICommand OpenClientMembershipView { get; }
    public ICommand OpenGymClassesView { get; }
    public ICommand OpenEmploymentTerminationsViewCommand { get; }
    public ICommand OpenStaffView { get; }
    public ICommand LogoutCommand { get; }
    public INavigationService Navigation { get; set; }

    public SidebarViewModel(INavigationService navigationService,AuthService authService)
    {
        _authService = authService;
        Navigation = navigationService;
        OpenRegisterViewCommand = new RelayCommand(o => Navigation.NavigateTo<RegisterViewModel>(), o => true);
        OpenClientView = new RelayCommand(o => Navigation.NavigateTo<ClientViewModel>(), o => true);
        OpenDashboardView = new RelayCommand(item => Navigation.NavigateTo<DashboardViewModel>(), o => true);
        OpenMembershipView = new RelayCommand(item => Navigation.NavigateTo<MembershipViewModel>(), o => true);
        OpenSettingsView = new RelayCommand(item => Navigation.NavigateTo<GeneralSettingsViewModel>(), o => true);
        OpenClientMembershipView = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipViewModel>(), item => true);
        OpenGymClassesView = new RelayCommand(item => Navigation.NavigateTo<GymClassViewModel>(), item => true);
        OpenEmploymentTerminationsViewCommand = new RelayCommand(item => Navigation.NavigateTo<EmploymentTerminationViewModel>(), item => true);
        OpenStaffView = new RelayCommand(item => Navigation.NavigateTo<StaffViewModel>(), item => true);
        LogoutCommand = new RelayCommand(item => Logout(), item => true);
    }

    private void Logout()
    {
        _authService.ClearJwt();
        Navigation.NavigateTo<LoginViewModel>();
    }
}
