using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.TrainerAvailability;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Trainer;

public class TrainerDetailsViewModel : ViewModel, IParameterReceiver
{
    private readonly TrainerHttpClient _trainerHttpClient;
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private TrainerDetailsResponse _trainer;


    public TrainerDetailsResponse Trainer
    {
        get { return _trainer; }
        set { _trainer = value; OnPropertyChanged(); }
    }

    public ICommand CreateTrainerAvailability { get; }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            _ = LoadTrainer(id);
        }
    }

    private async Task LoadTrainer(Guid id)
    {
        Result<TrainerDetailsResponse> result = await _trainerHttpClient.GetTrainer(id);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        else
        {
            Trainer = result.Value!;
        }
    }
    public TrainerDetailsViewModel(TrainerHttpClient trainerHttpClient, SidebarViewModel sidebarView, INavigationService navigation )
    {
        _trainerHttpClient = trainerHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        Trainer = new TrainerDetailsResponse();
        CreateTrainerAvailability = new RelayCommand(item => Navigation.NavigateTo<TrainerAvailabilityAddViewModel>(item), item => true);
    }
}
