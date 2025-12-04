using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Trainer;
using GymManagementSystem.WPF.ViewModels.TrainerContract;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Employee;

public class EmployeeDecisionViewModel : ViewModel
{
    private INavigationService _navigation;

    public EmployeeDecisionViewModel(INavigationService navigation, SidebarViewModel sidebarView)
    {
        Navigation = navigation;
        SidebarView = sidebarView;
        OpenEmployeeAddViewCommand = new RelayCommand(item => Navigation.NavigateTo<EmployeeAddViewModel>(), item => true);
        OpenTrainerAddViewCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerContractAddViewModel>(), item => true);
    }

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }


    public SidebarViewModel SidebarView { get; set; }


    public ICommand OpenEmployeeAddViewCommand { get; }
    public ICommand OpenTrainerAddViewCommand { get; }
}
