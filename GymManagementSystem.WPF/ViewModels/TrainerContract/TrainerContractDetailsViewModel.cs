using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.PdfGenerators;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Auth;
using GymManagementSystem.WPF.ViewModels.Trainer;
using GymManagementSystem.WPF.ViewModels.TrainerRate;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.TrainerContract;

public class TrainerContractDetailsViewModel : ViewModel, IParameterReceiver
{
    private readonly EmploymentTerminationHttpClient _employmentTerminationHttpClient;

    private readonly TrainerHttpClient _trainerHttpClient;
    public SidebarViewModel SidebarView { get; }

    public INavigationService Navigation { get; set; }

    private TrainerContractDetailsResponse _trainer = new();

    public ICommand GenerateTerminationCommand { get; }
    public TrainerContractDetailsResponse TrainerContract
    {
        get { return _trainer; }
        set { _trainer = value; OnPropertyChanged(); }
    }
    private Guid _trainerId;
    public ICommand OpenTrainerScheduleCommand { get; }
    public ICommand OpenTrainerRatesCommand { get; }
    public ICommand OpenSetNewPasswordViewCommand { get; }
    public ICommand LoadTrainerCommand { get; }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _trainerId = id;
        }
    }

    private async Task LoadTrainer()
    {
        Result<TrainerContractDetailsResponse> result = await _trainerHttpClient.GetTrainerContractAsync(_trainerId, true);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}");
        }
        else
        {
            TrainerContract = result.Value!;
        }
    }
    public TrainerContractDetailsViewModel(EmploymentTerminationHttpClient employmentTerminationHttpClient, TrainerHttpClient trainerHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _employmentTerminationHttpClient = employmentTerminationHttpClient;
        _trainerHttpClient = trainerHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        OpenTrainerScheduleCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerScheduleViewModel>(item!), item => true);
        OpenTrainerRatesCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerRateViewModel>(item!), item => true);
        GenerateTerminationCommand = new AsyncRelayCommand(item => GenerateEmploymentTerminationAsync(), item => true);
        OpenSetNewPasswordViewCommand = new RelayCommand(item => Navigation.NavigateTo<SetNewPasswordViewModel>(TrainerContract.PersonId), item => true);
        LoadTrainerCommand = new AsyncRelayCommand(item => LoadTrainer(), item => true);
    }

    private async Task GenerateEmploymentTerminationAsync()
    {
        Result<EmploymentTerminationGenerateResponse> result = await _employmentTerminationHttpClient.GetEmploymentTerminationDetailsAsync(TrainerContract.PersonId);
        if (result.IsSuccess)
        {
            EmploymentTerminationGenerator.GenerateEmploymentTerminationPdf(result.Value!);
            MessageBoxResult messageResult = MessageBox.Show("Is termination signed?", "Is signed?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageResult == MessageBoxResult.Yes)
            {
                EmploymentTerminationAddRequest request = new EmploymentTerminationAddRequest
                {
                    PersonId = TrainerContract.PersonId,
                    EffectiveDate = result.Value!.EffectiveDate
                };

                Result<Unit> additionResult = await _employmentTerminationHttpClient.CreateEmploymentTerminationAsync(request);
                if (!additionResult.IsSuccess)
                {
                    MessageBox.Show($"{additionResult.GetUserMessage()}");
                }
            }

            Navigation.NavigateTo<TrainerContractDetailsViewModel>(TrainerContract.Id);

        }
        else
        {
            MessageBox.Show($"{result.GetUserMessage()}");
        }
    }
}
