using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduleViewModels;
public class AddBookingDialogViewModel : ViewModel
{
    private readonly Guid _trainerId;
    private readonly PersonalBookingHttpClient _bookingHttp;
    private readonly ClientHttpClient _clientHttp;

    public ObservableCollection<ClientInfoResponse> Clients { get; set; } = new();
    private ClientInfoResponse _selectedClient;

    public ClientInfoResponse SelectedClient
    {
        get { return _selectedClient; }
        set { _selectedClient = value; OnPropertyChanged(); }
    }
    private ClassBookingAddRequest _request;

    private string _searchQuery;

    public string SearchQuery
    {
        get { return _searchQuery; }
        set { _searchQuery = value; OnPropertyChanged(); _ = DebouncedSearchAsync(_searchQuery); }
    }

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
            {
                Clients.Clear();
                return;
            }

            var results = await _clientHttp.LookUpClients(query, null);

            Clients.Clear();
            foreach (var r in results.Value!)
                Clients.Add(r);
        }
        catch (TaskCanceledException)
        {
        }
    }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public ICommand SaveCommand { get; }
    public event Action<bool>? CloseRequested;

    public AddBookingDialogViewModel(Guid trainerId, DateTime start, DateTime end,
        PersonalBookingHttpClient bookingHttp, ClientHttpClient clientHttp)
    {
        _trainerId = trainerId;
        _bookingHttp = bookingHttp;
        _clientHttp = clientHttp;

        StartTime = start;
        EndTime = end;

        SaveCommand = new RelayCommand(_ => Save(), _ => SelectedClient != null);

        //_ = LoadClientsAsync();
    }

    //private async Task LoadClientsAsync()
    //{
    //    var result = await _clientHttp.GetAllClientsAsync();
       
        
    //        foreach (var c in result)
    //            Clients.Add(c);
        
    //}

    private async void Save()
    {
        if (SelectedClient == null)
        {
            MessageBox.Show("SelectedClient is NULL");
            return;
        }

        MessageBox.Show($"Client ID being sent: {SelectedClient.Id}");

        CloseRequested?.Invoke(true);
    }

    public PersonalBookingAddRequest BuildDto()
    {
        return new PersonalBookingAddRequest
        {
            TrainerId = _trainerId,
            ClientId = SelectedClient!.Id,
            Start = DateTime.SpecifyKind(StartTime, DateTimeKind.Utc),
            End = DateTime.SpecifyKind(EndTime, DateTimeKind.Utc),
        };
    }
}
