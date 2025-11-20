using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.ServiceContracts;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private INavigationService _navigationService;
        public INavigationService Navigation
        {
            get { return _navigationService; }
            set
            {
                _navigationService = value; OnPropertyChanged();
            }
        }
        public ICommand OpenRegisterViewCommand { get;  }

        public MainWindowViewModel(INavigationService navigationService)
        {
            
            _navigationService = navigationService;
            Navigation.NavigateTo<LoginViewModel>();
        }


    }
}
