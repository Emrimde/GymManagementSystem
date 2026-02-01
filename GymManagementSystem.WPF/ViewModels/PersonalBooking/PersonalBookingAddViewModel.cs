using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
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
    private TimeSpan? _selectedStartSlot = null;

    public TimeSpan? SelectedStartSlot
    {
        get { return _selectedStartSlot; }
        set
        {
            _selectedStartSlot = value;
            if (value != null)
            {
                PersonalBookingAdd.StartHour = value.Value;
            }
            OnPropertyChanged();
        }
    }

    private TrainerRateSelectResponse? _selectedTrainerRate = null;

    public TrainerRateSelectResponse? SelectedTrainerRate
    {
        get { return _selectedTrainerRate; }
        set
        {
            _selectedTrainerRate = value;
            if (value != null)
            {
                PersonalBookingAdd.TrainerRateId = value.TrainerRateId;
            }
            OnPropertyChanged();
        }
    }

    public ICommand AddPersonalTrainingCommand { get; }
    private ObservableCollection<TrainerInfoResponse> _personalTrainers = new();

    public ObservableCollection<TrainerInfoResponse> PersonalTrainers
    {
        get { return _personalTrainers; }
        set { _personalTrainers = value; OnPropertyChanged(); }
    }

    private TrainerInfoResponse? _selectedPersonalTrainer = null;

    public TrainerInfoResponse? SelectedPersonalTrainer
    {
        get { return _selectedPersonalTrainer; }
        set
        {
            _selectedPersonalTrainer = value;
            if (value != null)
            {
                PersonalBookingAdd.TrainerId = value.Id;
            }
            LoadTrainerRatesCommand.Execute(this);
            OnPropertyChanged();
        }
    }

    public ICommand LoadTrainerRatesCommand { get; }
    public ICommand LoadPersonalTrainersCommand { get; }
    public ICommand CancelCommand { get; }

    private ObservableCollection<TrainerRateSelectResponse> _trainerRates = new();
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
        SidebarView = sidebarView;
        _personalBookingHttpClient = personalBookingHttpClient;
        _trainerHttpClient = trainerHttpClient;

        PersonalBookingAdd = new PersonalBookingAddRequest(); // OK tylko TU
        SelectedDate = DateTime.UtcNow;
        TimeSlots = GenerateTimeSlots();

        LoadTrainerRatesCommand = new AsyncRelayCommand(_ => LoadTrainerRatesAsync(), item=> true);
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(ClientId), item => true);
        LoadPersonalTrainersCommand = new AsyncRelayCommand(_ => LoadPersonalTrainers());
        AddPersonalTrainingCommand = new AsyncRelayCommand(
            item => AddPersonalTrainingAsync(),
            item => SelectedPersonalTrainer != null
              && SelectedTrainerRate != null
              && SelectedStartSlot != null);
    }

    private async Task AddPersonalTrainingAsync()
    {
        Result<PersonalBookingInfoResponse> result = await _personalBookingHttpClient.CreateAsync(PersonalBookingAdd);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}","Error",MessageBoxButton.OK,MessageBoxImage.Error);
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

    private async Task LoadTrainerRatesAsync()
    {
        if (SelectedPersonalTrainer != null)
        {
            Result<ObservableCollection<TrainerRateSelectResponse>> result = await _trainerHttpClient.GetTrainerRatesSelectAsync(SelectedPersonalTrainer.Id);
            if (result.IsSuccess)
            {
                TrainerRates = result.Value!;
            }
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
