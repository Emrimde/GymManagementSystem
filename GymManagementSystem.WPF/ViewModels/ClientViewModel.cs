using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels;

public class ClientViewModel : ViewModel
{
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;
    public ICommand OpenAddClientView { get; }
    public ICommand OpenEditClientCommand { get; }
    public ICommand OpenClientDetailsCommand { get; }
    public ICommand OpenAddClientMembershipViewCommand { get; }

    private readonly ClientHttpClient _clientHttpClient;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<ClientResponse> _clients;

    public ObservableCollection<ClientResponse> Clients
    {
        get { return _clients; }
        set
        {
            if (_clients != value)
            {
                _clients = value;
                OnPropertyChanged();
            }
        }
    }


    public ClientViewModel(INavigationService navigationService, SidebarViewModel sidebarView, ClientHttpClient clientHttpClient)
    {
        _navigation = navigationService;
        SidebarView = sidebarView;
        _clientHttpClient = clientHttpClient;
        Clients = new ObservableCollection<ClientResponse>();
        _ = LoadClientsAsync();
        OpenAddClientView = new RelayCommand((item) => Navigation.NavigateTo<ClientAddViewModel>(), item => true);
        OpenEditClientCommand = new RelayCommand(
    item =>
    {
        if (item is ClientResponse client)
            _navigation.NavigateTo<ClientUpdateViewModel>(client);
    }, item => true);

        OpenClientDetailsCommand = new RelayCommand(item =>
        {
            if (item is Guid id)
                Navigation.NavigateTo<ClientDetailsViewModel>(id);
        }, item => true);

        OpenAddClientMembershipViewCommand = new RelayCommand(item =>
        

        Navigation.NavigateTo<ClientMembershipAddViewModel>(item), item => true); 
    }

    private async Task LoadClientsAsync()
    {
        Clients = await _clientHttpClient.GetAllClientsAsync();
    }
}
