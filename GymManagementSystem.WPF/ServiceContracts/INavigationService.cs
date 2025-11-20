using GymManagementSystem.WPF.Core;

namespace GymManagementSystem.WPF.ServiceContracts;

public interface INavigationService
{
    ViewModel CurrentView { get;  }
    void NavigateTo<TViewModel>(object parameter = null) where TViewModel : ViewModel; 
}
