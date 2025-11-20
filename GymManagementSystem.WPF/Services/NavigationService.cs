using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.ServiceContracts;

namespace GymManagementSystem.WPF.Services;

class NavigationService : ObservableObject, INavigationService
{
    private readonly Func<Type, ViewModel> _viewModelFactory;
    private ViewModel? _currentView;
    public ViewModel CurrentView { get => _currentView!;
        set
        {
           _currentView = value;
            OnPropertyChanged();
        }

    }
    public NavigationService(Func<Type, ViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<TViewModel>(object parameter = null) where TViewModel : ViewModel
    {
        ViewModel viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
        if (parameter is not null && viewModel is IParameterReceiver receiver)
        {
            receiver.ReceiveParameter(parameter);
        }
        CurrentView = viewModel;
    }
}
