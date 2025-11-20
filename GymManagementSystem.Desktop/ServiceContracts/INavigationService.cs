using GymManagementSystem.Desktop.ViewModel;
using System.Windows.Controls;

namespace GymManagementSystem.Desktop.ServiceContracts;

public interface INavigationService
{
    ViewModelBase? CurrentViewModel { get; set; }
    void NavigateView<TViewModel>() where TViewModel : ViewModelBase;
}
