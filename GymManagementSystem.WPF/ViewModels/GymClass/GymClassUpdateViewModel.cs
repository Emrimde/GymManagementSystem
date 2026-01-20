using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.GymClass.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.GymClass;
public class GymClassUpdateViewModel : ViewModel, IParameterReceiver
{
    private readonly GymClassHtppClient _gymHttpClient;
    private GymClassEditFormModel _form = new();

    public GymClassEditFormModel Form
    {
        get { return _form; }
        set { _form = value; OnPropertyChanged(); }
    }

    private Guid _gymClassId; 
    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid gymClassId)
        {
            _gymClassId = gymClassId;
        }
    }

    private async Task LoadTrainerContracts()
    {
        Result<ObservableCollection<TrainerContractInfoResponse>> result = await _trainerHttpClient.GetInstructors();
        if (result.IsSuccess)
        {
            foreach (var item in result.Value!)
            {
                TrainerContracts.Add(item);
            }
        }
        else
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
    }



    public ObservableCollection<DayItem> DaysOfWeekItems { get; }

    public DaysOfWeekFlags SelectedDays =>
        DaysOfWeekItems.Where(item => item.IsSelected)
                       .Aggregate(DaysOfWeekFlags.None, (acc, d) => acc | d.Day);


    public SidebarViewModel SidebarView { get; set; }
    public ObservableCollection<TrainerContractInfoResponse> TrainerContracts { get; set; } = new();

    private TrainerContractInfoResponse? _selectedTrainerContract;
    private TrainerHttpClient _trainerHttpClient;

    public TrainerContractInfoResponse? SelectedTrainerContract
    {
        get { return _selectedTrainerContract; }
        set
        {
            _selectedTrainerContract = value;
            Form.TrainerContractId = value?.Id;
            OnPropertyChanged();
        }
    }

    public INavigationService Navigation { get; }

    public ICommand LoadGymClassCommand { get; }
    public ICommand LoadTrainerContractsCommand { get; }
    public GymClassUpdateViewModel(GymClassHtppClient gymHttpClient, SidebarViewModel sidebarView, INavigationService navigation, TrainerHttpClient trainerHttpClient)
    {
        _gymHttpClient = gymHttpClient;
        LoadGymClassCommand = new AsyncRelayCommand(item => LoadGymClassAsync(_gymClassId), item => true);
        LoadTrainerContractsCommand = new AsyncRelayCommand(item => LoadTrainerContracts(), item => true);
        SidebarView = sidebarView;
        Navigation = navigation;
        _trainerHttpClient = trainerHttpClient;
    }

    private async Task LoadGymClassAsync(Guid gymClassId)
    {
       Result<GymClassForEditResponse> result = await _gymHttpClient.GetGymClassForEdit(gymClassId);
        Form.StartHour = result.Value!.StartHour;
        Form.TrainerContractId = result.Value.TrainerContractId;
        Form.DaysOfWeek = result.Value.DaysOfWeek;
        Form.MaxPeople = result.Value.MaxPeople;
        Form.Name = result.Value.Name;
    }
}
