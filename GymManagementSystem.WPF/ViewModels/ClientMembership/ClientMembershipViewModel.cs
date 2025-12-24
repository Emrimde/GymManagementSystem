using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;

public class ClientMembershipViewModel : ViewModel, IParameterReceiver
{
    public Guid ClientId { get; set; }
    public ICommand OpenClientMembershipDetails { get; }
    private ClientInfoResponse _client;

    public ClientInfoResponse Client
    {
        get { return _client; }
        set { _client = value; OnPropertyChanged(); }
    }

    private readonly ClientMembershipHttpClient _httpClient;
    private readonly ClientHttpClient _clientHttpClient;
    private ObservableCollection<ClientMembershipResponse> _clientMemberships;
    public ICommand ReturnToClientDetailsViewCommand { get;  }

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
        Client = new ClientInfoResponse();
        _clientHttpClient = clientHttpClient;
        OpenClientMembershipDetails = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipDetailsViewModel>(item), item => true);
        ReturnToClientDetailsViewCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(ClientId), item => true);
    }

    private async Task LoadClientMemberships(Guid id)
    {
        ClientMemberships = await _httpClient.GetClientMembershipsHistoryAsync(id);
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            ClientId = id;
           _ = LoadClientMemberships(id);
            _ = LoadClientNameById(id);
        }
    }

    private async Task LoadClientNameById(Guid id)
    {
        Client = await _clientHttpClient.GetClientNameById(id);
    }

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }
    public SidebarViewModel SidebarView { get; }

    

}
