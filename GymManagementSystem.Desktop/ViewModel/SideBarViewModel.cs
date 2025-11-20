using GymManagementSystem.Desktop.ServiceContracts;
using GymManagementSystem.Desktop.Views.UserControls;
using System.Windows.Input;

namespace GymManagementSystem.Desktop.ViewModel;
public class SideBarViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    public SideBarViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        //OpenClientView = new RelayCommand(_ => _navigationService.NavigateView<>();
    }
    public ICommand OpenClientView { get; }
}
