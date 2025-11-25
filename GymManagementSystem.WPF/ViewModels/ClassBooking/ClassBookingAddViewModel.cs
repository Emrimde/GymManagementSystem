using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ScheduledClass;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
namespace GymManagementSystem.WPF.ViewModels.ClassBooking;

public class ClassBookingAddViewModel : ViewModel, IParameterReceiver
{
    private readonly ClientHttpClient _clientHttpClient;
    private readonly ClassBookingHttpClient _classBookingHttpClient;
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    
    public ObservableCollection<ClientInfoResponse> ClientSuggestions { get; set; }

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
                ClientSuggestions.Clear();
                return;
            }

            var results = await _clientHttpClient.LookUpClients(query, ScheduledClassId);

            ClientSuggestions.Clear();
            foreach (var r in results.Value!)
                ClientSuggestions.Add(r);
        }
        catch (TaskCanceledException)
        {
        }
    }

    public SidebarViewModel SidebarView { get; set; }
    public Guid ScheduledClassId { private get; set; }
    public ICommand AddClassBookingCommand { get; }
    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            ScheduledClassId = id;
            _request.ScheduledClassId = id;
        }
    }

    public ClassBookingAddViewModel(
       SidebarViewModel sidebar,
       ClientHttpClient clientHttpClient,
       INavigationService nav,
       ClassBookingHttpClient classBookingHttpClient)
    {
        SidebarView = sidebar;
        _clientHttpClient = clientHttpClient;
        Navigation = nav;
        ClientSuggestions = new ObservableCollection<ClientInfoResponse>();
        AddClassBookingCommand = new AsyncRelayCommand(item => AddClassBooking(), item => true);
        _classBookingHttpClient = classBookingHttpClient;
        _request = new ClassBookingAddRequest();
    }

    private async Task AddClassBooking()
    {
        _request.ClientId = SelectedClient.Id;
        Result<ClassBookingInfoResponse> result = await _classBookingHttpClient.PostClassBookingAsync(_request);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"Error: {result.ErrorMessage}");
        }
        Navigation.NavigateTo<ScheduledClassViewModel>();
    }
}
