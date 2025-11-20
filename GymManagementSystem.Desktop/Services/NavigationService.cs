using GymManagementSystem.Desktop.ServiceContracts;
using GymManagementSystem.Desktop.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GymManagementSystem.Desktop.Services;

public class NavigationService : INavigationService, INotifyPropertyChanged
{
    private readonly IServiceProvider serviceProvider;
    private ViewModelBase? _current;
    public ViewModelBase? CurrentViewModel { get => _current; set { _current = value; OnPropertyChanged(); } }
    public NavigationService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    private MainViewModel? _mainViewModel;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void NavigateView<TViewModel>() where TViewModel : ViewModelBase
    {
        CurrentViewModel = serviceProvider.GetRequiredService<TViewModel>();
    }
}

