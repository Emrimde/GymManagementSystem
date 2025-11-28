using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using System.Collections.ObjectModel;
using System.Windows.Input;

public class AddBookingDialogViewModel : ViewModel
{
    private readonly Guid _trainerId;
    private readonly PersonalBookingHttpClient _bookingHttp;
    private readonly ClientHttpClient _clientHttp;

    public ObservableCollection<ClientResponse> Clients { get; set; } = new();
    public ClientResponse? SelectedClient { get; set; }

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

        _ = LoadClientsAsync();
    }

    private async Task LoadClientsAsync()
    {
        var result = await _clientHttp.GetAllClientsAsync();
       
        
            foreach (var c in result)
                Clients.Add(c);
        
    }

    private async void Save()
    {
        if (SelectedClient == null) return;

        CloseRequested?.Invoke(true);
    }

    public PersonalBookingAddRequest BuildDto()
    {
        return new PersonalBookingAddRequest
        {
            TrainerId = _trainerId,
            ClientId = SelectedClient!.Id,
            Start = StartTime.ToUniversalTime(),
            End = EndTime.ToUniversalTime()
        };
    }
}
