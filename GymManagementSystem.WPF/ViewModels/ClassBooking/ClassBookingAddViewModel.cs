using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
namespace GymManagementSystem.WPF.ViewModels.ClassBooking;

public class ClassBookingAddViewModel : ViewModel, IParameterReceiver
{
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private ObservableCollection<ClientInfoResponse> _clientSuggestions;
    public ObservableCollection<ClientInfoResponse> ClientSuggestions
    { get { return _clientSuggestions; } set
        {
            _clientSuggestions = value; OnPropertyChanged();
        }
    }

    private ClientInfoResponse _selectedClient;

    public ClientInfoResponse SelectedClient
    {
        get { return _selectedClient; }
        set { _selectedClient = value; OnPropertyChanged(); }
    }

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
            // czekaj aż użytkownik przestanie pisać
            await Task.Delay(350, token);

            if (string.IsNullOrWhiteSpace(query))
            {
                ClientSuggestions.Clear();
                return;
            }

            var results = await FetchClientsAsync(query);

            ClientSuggestions.Clear();
            foreach (var r in results)
                ClientSuggestions.Add(r);
        }
        catch (TaskCanceledException)
        {
            // ignorujemy – normalne przy debounce
        }
    }


    public SidebarViewModel SidebarView { get; set; }
    public Guid ScheduledClassId { private get; set; }
    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            ScheduledClassId = id;
        }
    }
}
