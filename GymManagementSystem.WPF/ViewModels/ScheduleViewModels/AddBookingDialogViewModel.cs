using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

public class AddBookingDialogViewModel : ViewModel
{
    private readonly Guid _trainerId;
    private readonly PersonalBookingHttpClient _bookingHttp;
    private readonly ClientHttpClient _clientHttp;

    public ObservableCollection<ClientInfoResponse> Clients { get; set; } = new();

    public ClientInfoResponse SelectedClient
    {
        get => _selectedClient;
        set { _selectedClient = value; OnPropertyChanged(); }
    }
    private ClientInfoResponse _selectedClient;
    private ClassBookingAddRequest _request; private string _searchQuery; public string SearchQuery { get { return _searchQuery; } set { _searchQuery = value; OnPropertyChanged(); _ = DebouncedSearchAsync(_searchQuery); } }
    private CancellationTokenSource _debounceCts;

    private async Task DebouncedSearchAsync(string query)
    {
        _debounceCts?.Cancel();
        _debounceCts = new CancellationTokenSource();
        var token = _debounceCts.Token;
        try
        {
            await Task.Delay(350, token);
            if (string.IsNullOrWhiteSpace(query))
            { Clients.Clear(); return; }
            var results = await _clientHttp.LookUpClients(query, null);
            Clients.Clear();
            foreach (var r in results.Value!)
                Clients.Add(r);
        }
        catch (TaskCanceledException) { }
    }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    // --- NEW FIELDS ---
    public DateTime? SelectedDate { get; set; }
    public ObservableCollection<string> TimeSlots { get; set; }
    public string SelectedStartSlot { get; set; }
    public string SelectedEndSlot { get; set; }

    public ICommand SaveCommand { get; }
    public event Action<bool>? CloseRequested;

    public AddBookingDialogViewModel(Guid trainerId, DateTime start, DateTime end,
        PersonalBookingHttpClient bookingHttp, ClientHttpClient clientHttp)
    {
        _trainerId = trainerId;
        _bookingHttp = bookingHttp;
        _clientHttp = clientHttp;

        // Inicializacja 15-min slotów
        TimeSlots = GenerateTimeSlots();

        // Ustawienie start!
        SelectedDate = start.Date;
        SelectedStartSlot = start.ToString("HH:mm");
        SelectedEndSlot = end.ToString("HH:mm");

        SaveCommand = new RelayCommand(_ => Save(), _ => SelectedClient != null);
    }

    private ObservableCollection<string> GenerateTimeSlots()
    {
        var list = new ObservableCollection<string>();
        TimeSpan from = new TimeSpan(7, 0, 0);
        TimeSpan to = new TimeSpan(22, 0, 0);
        var step = TimeSpan.FromMinutes(15);

        for (var t = from; t <= to; t += step)
        {
            list.Add(t.ToString(@"hh\:mm"));
        }

        return list;
    }

    private void Save()
    {
        if (SelectedClient == null || SelectedDate == null)
        {
            MessageBox.Show("Missing client or date");
            return;
        }

        CloseRequested?.Invoke(true);
    }

    public PersonalBookingAddRequest BuildDto()
    {
        var startTimeSpan = TimeSpan.Parse(SelectedStartSlot);
        var endTimeSpan = TimeSpan.Parse(SelectedEndSlot);

        var start = SelectedDate.Value.Date + startTimeSpan;
        var end = SelectedDate.Value.Date + endTimeSpan;

        return new PersonalBookingAddRequest
        {
            TrainerId = _trainerId,
            ClientId = SelectedClient.Id,
            Start = DateTime.SpecifyKind(start, DateTimeKind.Utc),
            End = DateTime.SpecifyKind(end, DateTimeKind.Utc)
        };
    }
}
