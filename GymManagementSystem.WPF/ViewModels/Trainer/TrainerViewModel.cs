using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using Microsoft.Xaml.Behaviors.Core;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Trainer;
public class TrainerViewModel : ViewModel
{
    private readonly TrainerHttpClient _httpClient;
    public SidebarViewModel SidebarView { get; set; }
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private ObservableCollection<TrainerResponse> _trainers;
    public ObservableCollection<TrainerResponse> Trainers
    {
        get { return _trainers; }
        set { _trainers = value; OnPropertyChanged(); }
    }

    public ICommand OpenAddTrainerCommand { get; }


    public TrainerViewModel(TrainerHttpClient httpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _httpClient = httpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        Trainers = new ObservableCollection<TrainerResponse>();
        _ = LoadTrainers();
        OpenAddTrainerCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerAddViewModel>(), item => true);
    }

    private async Task LoadTrainers()
    {
        Result<ObservableCollection<TrainerResponse>> result = await _httpClient.GetTrainers();
        if (result.IsSuccess)
        {
            Trainers = result.Value!;
        }
        else
        {
            MessageBox.Show("Failed to load trainers");
        }
    }
}
