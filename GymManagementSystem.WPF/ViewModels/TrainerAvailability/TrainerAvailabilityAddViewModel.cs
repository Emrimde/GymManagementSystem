using GymManagementSystem.Core.DTO.TrainerAvailabilityTemplate;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Trainer;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.TrainerAvailability;

public class TrainerAvailabilityAddViewModel : ViewModel, IParameterReceiver
{
    private Guid _trainerId;
    private readonly TrainerHttpClient _trainerHttpClient;
    private TrainerAvailabilityAddRequest _trainerAvalAdd;

    public TrainerAvailabilityAddRequest TrainerAvailabilityAddRequest
    {
        get { return _trainerAvalAdd; }
        set { _trainerAvalAdd = value; OnPropertyChanged(); }
    }
    private INavigationService _navigation;


    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value;  OnPropertyChanged(); }
    }

    public SidebarViewModel SidebarView { get; set; }
    public ICommand CreateTrainerAvailabilityCommand { get; }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _trainerId = id;
            TrainerAvailabilityAddRequest.TrainerId = id;
        }
    }

    public TrainerAvailabilityAddViewModel(TrainerHttpClient trainerHttpClient, INavigationService navigation, SidebarViewModel sidebarView)
    {
        _trainerHttpClient = trainerHttpClient;
        Navigation = navigation;
        SidebarView = sidebarView;
        TrainerAvailabilityAddRequest = new TrainerAvailabilityAddRequest();
        CreateTrainerAvailabilityCommand = new AsyncRelayCommand(item => CreateTrainerAvailability(), item => true);
    }

    private async Task CreateTrainerAvailability()
    {
        Result<TrainerAvailabilityInfoResponse> result = await _trainerHttpClient.PostTrainerAvailabilityAsync(TrainerAvailabilityAddRequest);
        if (!result.IsSuccess) 
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        else
        {
            Navigation.NavigateTo<TrainerViewModel>();
        }
    }
}
