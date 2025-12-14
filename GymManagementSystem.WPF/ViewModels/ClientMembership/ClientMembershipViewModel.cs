using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;

public class ClientMembershipViewModel : ViewModel, IParameterReceiver
{

    public ICommand OpenClientMembershipDetails { get; }
    private ClientNameResponse _client;

    public ClientNameResponse Client
    {
        get { return _client; }
        set { _client = value; OnPropertyChanged(); }
    }

    private readonly ClientMembershipHttpClient _httpClient;
    private readonly ClientHttpClient _clientHttpClient;
    private ObservableCollection<ClientMembershipResponse> _clientMemberships;

    public ObservableCollection<ClientMembershipResponse> ClientMemberships
    {

        get { return _clientMemberships; }
        set { _clientMemberships = value; OnPropertyChanged(); }
    }

    public INavigationService _navigation;

    public ClientMembershipViewModel(ClientMembershipHttpClient httpClient, INavigationService navigation, SidebarViewModel sidebarView, ClientHttpClient clientHttpClient)
    {
        _httpClient = httpClient;
        Navigation = navigation;
        SidebarView = sidebarView;
        ClientMemberships = new ObservableCollection<ClientMembershipResponse>();
        Client = new ClientNameResponse();
        _clientHttpClient = clientHttpClient;
        //_ = LoadClientMemberships();
        OpenClientMembershipDetails = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipDetailsViewModel>(item), item => true);
    }

    private async Task LoadClientMemberships(Guid id)
    {
        ClientMemberships = await _httpClient.GetClientMembershipsHistoryAsync(id);
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
           _ = LoadClientMemberships(id);
            _ = LoadClientNameById(id);
        }
    }

    private async Task LoadClientNameById(Guid id)
    {
        ClientNameResponse? result = await _clientHttpClient.GetClientNameById(id);
        if(result != null)
        {
            Client = result;
            return;
        }

        MessageBox.Show("Failed to load client name.");
    }

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }
    public SidebarViewModel SidebarView { get; }

    

}
