using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Auth;

namespace GymManagementSystem.WPF.ViewModels;
public class MainWindowViewModel : ViewModel
{
   public INavigationService Navigation { get; set; }

    public MainWindowViewModel(INavigationService navigationService)
    {
        Navigation = navigationService;
        Navigation.NavigateTo<LoginViewModel>();
    }
}
