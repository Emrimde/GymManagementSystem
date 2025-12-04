using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Trainer;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.TrainerContract;

public class TrainerContractDetailsViewModel : ViewModel, IParameterReceiver
{

    private bool _isB2b;

    public bool IsB2B
    {
        get { return _isB2b; }
        set { _isB2b = value; OnPropertyChanged(); }
    }







    private readonly TrainerHttpClient _trainerHttpClient;
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private TrainerContractDetailsResponse _trainer;


    public TrainerContractDetailsResponse TrainerContract
    {
        get { return _trainer; }
        set { _trainer = value; OnPropertyChanged(); }
    }

    public ICommand OpenTrainerScheduleCommand { get; }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _ = LoadTrainer(id);
        }
    }

    private async Task LoadTrainer(Guid id)
    {
        Result<TrainerContractDetailsResponse> result = await _trainerHttpClient.GetTrainerContractAsync(id, true);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        else
        {
            TrainerContract = result.Value!;
        }
    }

    public TrainerContractDetailsViewModel(TrainerHttpClient trainerHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _trainerHttpClient = trainerHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        TrainerContract = new TrainerContractDetailsResponse();
        OpenTrainerScheduleCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerScheduleViewModel>(item), item => true);

    }
}
