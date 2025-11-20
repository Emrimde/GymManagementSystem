using GymManagementSystem.Desktop.ServiceContracts;
using GymManagementSystem.Desktop.Services;
using GymManagementSystem.Desktop.Views.UserControls;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GymManagementSystem.Desktop.ViewModel;

public class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    public MainViewModel(INavigationService navigationService)
    {
        //CurrentView = new LoginView
        //{
        //    DataContext = loginViewModel
        //};
        _navigationService = navigationService;
    }

    public ViewModelBase? CurrentViewModel
    {
        get => _navigationService.CurrentViewModel;
        private set
        {
            if (_navigationService.CurrentViewModel != value)
            {
                _navigationService.CurrentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }
    }
    public void ChangeView<TViewModel>(TViewModel newView) where TViewModel : ViewModelBase
    {

        CurrentViewModel = newView;
    }
}
