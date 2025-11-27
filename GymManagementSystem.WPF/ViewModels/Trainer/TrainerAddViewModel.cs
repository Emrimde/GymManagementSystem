using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Trainer;

public class TrainerAddViewModel : ViewModel
{
    private readonly TrainerHttpClient _httpClient;
    public SidebarViewModel SidebarView { get; set; }
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private TrainerAddRequest _trainerAdd;
    public TrainerAddRequest TrainerAddRequest
    {
        get { return _trainerAdd; }
        set { _trainerAdd = value; OnPropertyChanged(); }
    }
    public ICommand AddTrainerCommand { get; }

    public TrainerAddViewModel(TrainerHttpClient httpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _httpClient = httpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        TrainerAddRequest = new TrainerAddRequest();
        AddTrainerCommand = new AsyncRelayCommand(item => AddTrainerAsync(), item => true);


    }

    private async Task AddTrainerAsync()
    {
        Result<TrainerInfoResponse> result = await _httpClient.PostTrainerAsync(TrainerAddRequest);
        if (result.IsSuccess) 
        {
           
            Navigation.NavigateTo<TrainerDetailsViewModel>(result.Value!.Id);
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}");
        }
    }
}
