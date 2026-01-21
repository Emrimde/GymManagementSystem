using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.GymClass.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            Form.TrainerContractId = value!.Id;
            OnPropertyChanged();
        }
    }

    public INavigationService Navigation { get; }

    public ICommand LoadGymClassCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand LoadTrainerContractsCommand { get; }
    public ICommand EditGymClassCommand { get; }
    public GymClassUpdateViewModel(GymClassHtppClient gymHttpClient, SidebarViewModel sidebarView, INavigationService navigation, TrainerHttpClient trainerHttpClient)
    {
        DaysOfWeekItems = new ObservableCollection<DayItem>(
               Enum.GetValues(typeof(DaysOfWeekFlags))
                   .Cast<DaysOfWeekFlags>()
                   .Where(item => item != DaysOfWeekFlags.None)
                   .Select(item => new DayItem { Day = item, IsSelected = false })
           );

        foreach (var item in DaysOfWeekItems)
        {
            item.PropertyChanged += DayItem_PropertyChanged;
        }
        _gymHttpClient = gymHttpClient;
        Navigation = navigation;
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<GymClassViewModel>(), item => true);
        LoadGymClassCommand = new AsyncRelayCommand(item => LoadGymClassAsync(_gymClassId), item => true);
        LoadTrainerContractsCommand = new AsyncRelayCommand(item => LoadTrainerContracts(), item => true);
        EditGymClassCommand = new AsyncRelayCommand(item => UpdateGymClassAsync(), item => Form.IsFormComplete && !Form.HasErrors);
        SidebarView = sidebarView;
        _trainerHttpClient = trainerHttpClient;
    }

    private async Task UpdateGymClassAsync()
    {
        GymClassUpdateRequest gymClassUpdateRequest = new GymClassUpdateRequest()
        {
            DaysOfWeek = Form.DaysOfWeek,
            MaxPeople = Form.MaxPeople, 
            StartHour = Form.StartHour,
            TrainerId = Form.TrainerContractId,
            GymClassId = _gymClassId,
            Name = Form.Name,
        };
       Result<Unit> result = await _gymHttpClient.PutGymClassAsync(gymClassUpdateRequest);
        if (result.IsSuccess) 
        {
            Navigation.NavigateTo<GymClassViewModel>();
        }
        
    }

    private void DayItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(DayItem.IsSelected))
        {
            var newValue = SelectedDays;
            if (Form.DaysOfWeek != newValue)
                Form.DaysOfWeek = newValue;
        }
    }


    private void ApplyDaysFromFlags(DaysOfWeekFlags flags)
    {
        foreach (var item in DaysOfWeekItems)
        {
            item.IsSelected = flags.HasFlag(item.Day);
        }
    }

    private async Task LoadGymClassAsync(Guid gymClassId)
    {
        Result<GymClassForEditResponse> result =
            await _gymHttpClient.GetGymClassForEdit(gymClassId);


        if (!result.IsSuccess)
        {
            MessageBox.Show(result.ErrorMessage);
            return;
        }

        var dto = result.Value!;

        Form.StartHour = dto.StartHour;
        Form.TrainerContractId = dto.TrainerContractId;
        Form.DaysOfWeek = dto.DaysOfWeek;
        Form.MaxPeople = dto.MaxPeople;
        Form.Name = dto.Name;

        ApplyDaysFromFlags(dto.DaysOfWeek); // ⭐ klucz
    }

}
