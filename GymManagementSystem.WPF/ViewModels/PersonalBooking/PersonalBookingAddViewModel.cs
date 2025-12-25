using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using Syncfusion.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.PersonalBooking;

public class PersonalBookingAddViewModel : ViewModel, IParameterReceiver
{
    public INavigationService Navigation { get; set; }
    public SidebarViewModel SidebarView { get; set; }
    public PersonalBookingAddRequest PersonalBookingAdd { get; set; }
    public TrainerHttpClient _trainerHttpClient { get; set; }
    public DateTime SelectedDate
    {
        get => selectedDate;

        set
        {
            selectedDate = value;
            PersonalBookingAdd.StartDay = value;
        }
    }
    private readonly PersonalBookingHttpClient _personalBookingHttpClient;
    private TimeSpan _selectedStartSlot;

    public TimeSpan SelectedStartSlot
    {
        get { return _selectedStartSlot; }
        set
        {
            _selectedStartSlot = value;
            PersonalBookingAdd.StartHour = _selectedStartSlot;
        }
    }

    private TrainerRateSelectResponse _selectedTrainerRate;

    public TrainerRateSelectResponse SelectedTrainerRate
    {
        get { return _selectedTrainerRate; }
        set { _selectedTrainerRate = value;
            PersonalBookingAdd.TrainerRateId = value.TrainerRateId;
        }
    }

    public ICommand AddPersonalTrainingCommand { get; }
    private ObservableCollection<TrainerInfoResponse> _personalTrainers;

    public ObservableCollection<TrainerInfoResponse> PersonalTrainers
    {
        get { return _personalTrainers; }
        set { _personalTrainers = value; OnPropertyChanged(); }
    }

    private TrainerInfoResponse _selectedPersonalTrainer;

    public TrainerInfoResponse SelectedPersonalTrainer
    {
        get { return _selectedPersonalTrainer; }
        set
        {
            _selectedPersonalTrainer = value;
            PersonalBookingAdd.TrainerId = value.Id;
            _ = LoadTrainerRatesAsync(_selectedPersonalTrainer.Id);
        }
    }

    private ObservableCollection<TrainerRateSelectResponse> _trainerRates;
    private DateTime selectedDate;

    public ObservableCollection<TrainerRateSelectResponse> TrainerRates
    {
        get { return _trainerRates; }
        set { _trainerRates = value; OnPropertyChanged(); }
    }

    public ObservableCollection<TimeSpan> TimeSlots { get; set; }


    public PersonalBookingAddViewModel(INavigationService navigation, SidebarViewModel sidebarView, PersonalBookingHttpClient personalBookingHttpClient, TrainerHttpClient trainerHttpClient)
    {
        Navigation = navigation;
        PersonalBookingAdd = new PersonalBookingAddRequest();
        SelectedDate = DateTime.UtcNow;
        PersonalTrainers = new ObservableCollection<TrainerInfoResponse>();
        AddPersonalTrainingCommand = new AsyncRelayCommand(item => AddPersonalTrainingAsync(), item => true);
        TrainerRates = new ObservableCollection<TrainerRateSelectResponse>();
        SidebarView = sidebarView;
        TimeSlots = GenerateTimeSlots();
        this._personalBookingHttpClient = personalBookingHttpClient;
        _trainerHttpClient = trainerHttpClient;
        _ = LoadPersonalTrainers();
    }

    private async Task AddPersonalTrainingAsync()
    {
        Result<PersonalBookingInfoResponse> result = await _personalBookingHttpClient.CreateAsync(PersonalBookingAdd);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
            return;
        }
        Navigation.NavigateTo<ClientDetailsViewModel>(ClientId);
    }

    private ObservableCollection<TimeSpan> GenerateTimeSlots()
    {
        var list = new ObservableCollection<TimeSpan>();
        TimeSpan from = new TimeSpan(7, 0, 0);
        TimeSpan to = new TimeSpan(22, 0, 0);
        var step = TimeSpan.FromMinutes(15);

        for (var t = from; t <= to; t += step)
        {
            list.Add(t);
        }

        return list;
    }

    private async Task LoadPersonalTrainers()
    {
        Result<ObservableCollection<TrainerInfoResponse>> result = await _trainerHttpClient.GetPersonalTrainersAsync();
        if (result.IsSuccess)
        {
            PersonalTrainers = result.Value!;
        }
    }

    private async Task LoadTrainerRatesAsync(Guid id)
    {
        Result<ObservableCollection<TrainerRateSelectResponse>> result = await _trainerHttpClient.GetTrainerRatesSelectAsync(id);
        if (result.IsSuccess)
        {
            TrainerRates = result.Value!;
        }
    }

    public Guid ClientId { get; set; }
    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            ClientId = clientId;
            PersonalBookingAdd.ClientId = clientId;
        }
    }
}
