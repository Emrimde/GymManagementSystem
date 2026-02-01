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

public class PersonalBookingUpdateViewModel : ViewModel, IParameterReceiver
{
    private readonly PersonalBookingHttpClient _personalBookingHttpClient;
    private readonly TrainerHttpClient _trainerHttpClient;

    public INavigationService Navigation { get; }

    private Guid _personalBookingId;
    private Guid _clientId;

    // ====== WŁAŚCIWOŚCI STANU (bez DTO) ======

    private DateTime _selectedDate;
    public DateTime SelectedDate
    {
        get => _selectedDate;
        set { _selectedDate = value; OnPropertyChanged(); }
    }

    private TimeSpan? _selectedStartSlot;
    public TimeSpan? SelectedStartSlot
    {
        get => _selectedStartSlot;
        set { _selectedStartSlot = value; OnPropertyChanged(); }
    }

    private TrainerInfoResponse? _selectedTrainer;
    public TrainerInfoResponse? SelectedTrainer
    {
        get => _selectedTrainer;
        set
        {
            _selectedTrainer = value;
            LoadTrainerRatesCommand.Execute(null);
            OnPropertyChanged();
        }
    }

    private TrainerRateSelectResponse? _selectedTrainerRate;
    public TrainerRateSelectResponse? SelectedTrainerRate
    {
        get => _selectedTrainerRate;
        set { _selectedTrainerRate = value; OnPropertyChanged(); }
    }


    public ObservableCollection<TrainerInfoResponse> PersonalTrainers { get; set; } = new();
    public ObservableCollection<TrainerRateSelectResponse> TrainerRates { get; set; } = new();
    public ObservableCollection<TimeSpan> TimeSlots { get; }


    public ICommand UpdatePersonalTrainingCommand { get; }
    public ICommand LoadTrainerRatesCommand { get; }
    public ICommand LoadCommand { get; }
    public ICommand CancelCommand { get; }

    public PersonalBookingUpdateViewModel(
        INavigationService navigation,
        PersonalBookingHttpClient personalBookingHttpClient,
        TrainerHttpClient trainerHttpClient)
    {
        Navigation = navigation;
        _personalBookingHttpClient = personalBookingHttpClient;
        _trainerHttpClient = trainerHttpClient;

        TimeSlots = GenerateTimeSlots();

        UpdatePersonalTrainingCommand = new AsyncRelayCommand(
            item => UpdateAsync(),
            item => SelectedTrainer != null && SelectedTrainerRate != null && SelectedStartSlot != null
        );

        LoadTrainerRatesCommand = new AsyncRelayCommand(item => LoadTrainerRatesAsync());
        LoadCommand = new AsyncRelayCommand(item => LoadAsync());
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<PersonalBookingViewModel>(_clientId), item=> true);
    }

    private async Task UpdateAsync()
    {
        var request = new PersonalBookingUpdateRequest
        {
            PersonalBookingId = _personalBookingId,
            TrainerId = SelectedTrainer!.Id,
            TrainerRateId = SelectedTrainerRate!.TrainerRateId,
            ClientId = _clientId,
            StartDay = SelectedDate,
            StartHour = SelectedStartSlot!.Value
        };

        Result<PersonalBookingInfoResponse> result =
            await _personalBookingHttpClient.UpdateAsync(_clientId, request);

        if (!result.IsSuccess)
        {
            MessageBox.Show(result.GetUserMessage(), "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        Navigation.NavigateTo<ClientDetailsViewModel>(_clientId);
    }


    private async Task LoadTrainerRatesAsync()
    {
        if (SelectedTrainer == null)
            return;

        var result = await _trainerHttpClient
            .GetTrainerRatesSelectAsync(SelectedTrainer.Id);

        if (result.IsSuccess)
            TrainerRates = result.Value!;
    }
    private async Task LoadAsync()
    {
        if (_personalBookingId == Guid.Empty)
            return;

        Result<PersonalBookingForEditResponse> bookingResult = await _personalBookingHttpClient.GetPersonalBookingForEdit(_personalBookingId);
        if (!bookingResult.IsSuccess)
            return;

        PersonalBookingForEditResponse booking = bookingResult.Value!;

        _clientId = booking.ClientId;
        SelectedDate = booking.Start.Date;
        SelectedStartSlot = booking.Start.TimeOfDay;

        Result<ObservableCollection<TrainerInfoResponse>> trainersResult = await _trainerHttpClient.GetPersonalTrainersAsync();
        if (!trainersResult.IsSuccess)
            return;

        PersonalTrainers = trainersResult.Value!;

        SelectedTrainer = PersonalTrainers
            .FirstOrDefault(item => item.Id == booking.TrainerId);

        // 3. stawki
        await LoadTrainerRatesAsync();

        SelectedTrainerRate = TrainerRates
            .FirstOrDefault(item => item.TrainerRateId == booking.TrainerRateId);
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid personalBookingId)
        {
            _personalBookingId = personalBookingId;
        }
    }

    private ObservableCollection<TimeSpan> GenerateTimeSlots()
    {
        var list = new ObservableCollection<TimeSpan>();
        var from = new TimeSpan(7, 0, 0);
        var to = new TimeSpan(22, 0, 0);
        var step = TimeSpan.FromMinutes(15);

        for (var t = from; t <= to; t += step)
            list.Add(t);

        return list;
    }
}
