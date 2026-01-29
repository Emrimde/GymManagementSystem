using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.PdfGenerators;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Employee;
using GymManagementSystem.WPF.ViewModels.TrainerContract;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Staff;
public class StaffDetailsViewModel : ViewModel, IParameterReceiver
{
    private readonly StaffHttpClient _staffHttpClient;
    private readonly TrainerHttpClient _trainerHttpClient;
    private readonly GeneralGymDetailsHttpClient _generalGymHttpClient;
    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation { get; set; }
    public ICommand OpenEmployeeViewCommand { get; }
    public ICommand AddPersonalTrainerRoleCommand { get; }
    public ICommand AddGroupTrainerRoleCommand { get; }
    public ICommand LoadPersonDetailsCommand { get; }

    private PersonDetailsResponse _person = new();

    public StaffDetailsViewModel(StaffHttpClient staffHttpClient, SidebarViewModel sidebarView, INavigationService navigation, TrainerHttpClient trainerHttpClient, GeneralGymDetailsHttpClient generalGymHttpClient)
    {
        _staffHttpClient = staffHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        _generalGymHttpClient = generalGymHttpClient;
        OpenEmployeeViewCommand = new RelayCommand(item => Navigation.NavigateTo<EmployeeAddViewModel>(PersonId), item => true);
        AddPersonalTrainerRoleCommand = new AsyncRelayCommand(item => AddPersonalTrainerRoleAsync(TrainerTypeEnum.PersonalTrainer), item => true);
        AddGroupTrainerRoleCommand = new AsyncRelayCommand(item => AddGroupInstructorRoleAsync(TrainerTypeEnum.GroupInstructor), item => true);
        LoadPersonDetailsCommand = new AsyncRelayCommand(item => LoadPersonDetailsAsync(), item => true);
        Person = new PersonDetailsResponse();
        
        _trainerHttpClient = trainerHttpClient;
    }

    private async Task AddGroupInstructorRoleAsync(TrainerTypeEnum groupInstructor)
    {
        await AddTrainerAsync(groupInstructor);
    }

    private async Task AddPersonalTrainerRoleAsync(TrainerTypeEnum personalTrainer)
    {
        await AddTrainerAsync(personalTrainer);
    }

    public PersonDetailsResponse Person
    {
        get { return _person; }
        set { _person = value; OnPropertyChanged(); }
    }


    public Guid PersonId { get; set; }
    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid personId)
        {
            PersonId = personId;
        }
    }

    private async Task LoadPersonDetailsAsync()
    {
        Result<PersonDetailsResponse> result = await _staffHttpClient.GetPersonDetailsAsync(PersonId);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.GetUserMessage());
            return;
        }
        Person = result.Value!;
    }

    private async Task AddTrainerAsync(TrainerTypeEnum trainerType)
    {
        TrainerContractAddRequest request = new TrainerContractAddRequest()
        {
            PersonId = PersonId,
            ContractType = ContractTypeEnum.ContractOfMandate,
            TrainerType = trainerType,
            ClubCommissionPercent = 10m
        };

        Result<GeneralGymResponse> generalGymResult = await _generalGymHttpClient.GetGeneralGymSettingsAsync();
        if (!generalGymResult.IsSuccess)
        {
            MessageBox.Show("Error during loading gym data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        request.GenerateTrainerContractPdf(generalGymResult.Value!, Person);
        MessageBoxResult response = MessageBox.Show("Is trainer signed contract?", "Confirmation", MessageBoxButton.YesNo);
        if (response == MessageBoxResult.Yes)
        {
            Result<TrainerContractInfoResponse> result = await _trainerHttpClient.PostTrainerContractAsync(request);
            if (result.IsSuccess)
            {
                Navigation.NavigateTo<TrainerContractDetailsViewModel>(result.Value!.Id);
            }
            else
            {
                MessageBox.Show($"{result.GetUserMessage()}");
            }
        }
    }
}
