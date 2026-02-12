using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;

public class ClientMembershipViewModel : ViewModel, IParameterReceiver
{
    public Guid ClientId { get; set; }
    public ICommand OpenClientMembershipDetails { get; }
    private ClientInfoResponse _client = new();

    public ClientInfoResponse Client
    {
        get { return _client; }
        set { _client = value; OnPropertyChanged(); }
    }

    private readonly ClientMembershipHttpClient _httpClient;
    private readonly ClientHttpClient _clientHttpClient;
    private ObservableCollection<ClientMembershipResponse> _clientMemberships = new();
    public ICommand ReturnToClientDetailsViewCommand { get;  }
    public ICommand LoadClientMembershipsDataCommand { get;  }

    public ObservableCollection<ClientMembershipResponse> ClientMemberships
    {

        get { return _clientMemberships; }
        set { _clientMemberships = value; OnPropertyChanged(); }
    }


    public ClientMembershipViewModel(ClientMembershipHttpClient httpClient, INavigationService navigation, SidebarViewModel sidebarView, ClientHttpClient clientHttpClient)
    {
        _httpClient = httpClient;
        Navigation = navigation;
        SidebarView = sidebarView;
        ClientMemberships = new ObservableCollection<ClientMembershipResponse>();
        Client = new ClientInfoResponse();
        _clientHttpClient = clientHttpClient;
        OpenClientMembershipDetails = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipDetailsViewModel>(item!), item => true);
        ReturnToClientDetailsViewCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(ClientId), item => true);
        LoadClientMembershipsDataCommand = new AsyncRelayCommand(item => LoadClientMembershipsDataAsync(), item => true);
    }

    private async Task LoadClientMembershipsDataAsync()
    {
        await LoadClientMemberships(ClientId);
        await LoadClientNameById(ClientId);
    }

    private async Task LoadClientMemberships(Guid id)
    {
        Result<ObservableCollection<ClientMembershipResponse>> result = await _httpClient.GetClientMembershipsHistoryAsync(id);
        ClientMemberships = result.Value!;
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            ClientId = id;
        }
    }

    private async Task LoadClientNameById(Guid id)
    {
        Result<ClientInfoResponse> result = await _clientHttpClient.GetClientNameById(id);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Client = result.Value!;
    }

    public INavigationService Navigation { get; set; }

    public SidebarViewModel SidebarView { get; }
    
}
