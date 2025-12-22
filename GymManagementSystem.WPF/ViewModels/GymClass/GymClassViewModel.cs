using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ScheduledClass;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.GymClass;
public class GymClassViewModel : ViewModel
{
    private readonly GymClassHtppClient _httpClient;
    public SidebarViewModel SidebarView { get; set; }
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private ObservableCollection<GymClassResponse> _gymClasses;

    public ICommand OpenAddGymClassCommand { get; }
    public ICommand OpenScheduledClassesViewCommand { get; }
    public GymClassViewModel(GymClassHtppClient httpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _httpClient = httpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        OpenAddGymClassCommand = new RelayCommand(item => Navigation.NavigateTo<GymClassAddViewModel>(), item => true);
        OpenScheduledClassesViewCommand = new RelayCommand(item => Navigation.NavigateTo<ScheduledClassViewModel>(item), item => true);
        GymClasses = new ObservableCollection<GymClassResponse>();
        _ = LoadGymClasses();
    }

    private async Task LoadGymClasses()
    {
       Result<ObservableCollection<GymClassResponse>> result = await _httpClient.GetGymClasses();
        if (result.IsSuccess)
        {
            GymClasses = result.Value!;
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}");
        }
    }

    public ObservableCollection<GymClassResponse> GymClasses
    {
        get { return _gymClasses; }
        set { _gymClasses = value; OnPropertyChanged(); }
    }

}
