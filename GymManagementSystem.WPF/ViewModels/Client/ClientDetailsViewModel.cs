using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using GymManagementSystem.WPF.ViewModels.Termination;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;
public class ClientDetailsViewModel : ViewModel, IParameterReceiver
{
    public string ActiveMembershipName =>
    Client.IsActive
        ? $"{Client.ClientMembership?.Membership?.Name} {Client.ClientMembership?.Membership?.MembershipType}"
        : "No membership";

    private INavigationService _navigation;
    public ICommand CreateNewTerminationCommand { get; }
    public ICommand OpenClientMembershipsHistory { get; }
    public ICommand OpenAddClientMembershipViewCommand { get; }
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    public Guid ClientId { get; set; }
    private ClientDetailsResponse _client;
    public ClientDetailsResponse Client
    {
        get => _client;
        set
        {
            _client = value; OnPropertyChanged();
            OnPropertyChanged(nameof(ActiveMembershipName));
        }
    }

    private TerminationAddRequest _terminationAdd;

    public TerminationAddRequest TerminationAddRequest
    {
        get { return _terminationAdd; }
        set
        {
            _terminationAdd = value;
            OnPropertyChanged();
        }
    }


    private readonly ClientHttpClient _clienthttpClient;
    public SidebarViewModel SidebarView { get; }

    public ClientDetailsViewModel(SidebarViewModel sidebarViewModel, INavigationService navigation, ClientHttpClient clientHttpClient)
    {
        _clienthttpClient = clientHttpClient;
        Navigation = navigation;
        Client = new ClientDetailsResponse();
        TerminationAddRequest = new TerminationAddRequest();
        OpenClientMembershipsHistory = new RelayCommand(item =>
            Navigation.NavigateTo<ClientMembershipViewModel>(ClientId), item => true);


        SidebarView = sidebarViewModel;
        CreateNewTerminationCommand = new RelayCommand(item =>

         OpenCreateNewTermination()
        , item => true);


        OpenAddClientMembershipViewCommand = new RelayCommand(item =>
        {
            if (item is Guid id)
            {
                Navigation.NavigateTo<ClientMembershipAddViewModel>(item);
            }
            else
            {
                MessageBox.Show("Fail");
            }

        }
           , item => true);
    }

    private async Task LoadClient()
    {
        Result<ClientDetailsResponse> result = await _clienthttpClient.GetClientById(ClientId, true);
        if (result.IsSuccess)
        {
            Client = result.Value!;
            TerminationAddRequest.ContractId = Client?.ClientMembership?.ContractId ?? Guid.Empty;
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}");
        }
    }

    private void OpenCreateNewTermination()
    {
        Navigation.NavigateTo<TerminationAddViewModel>(TerminationAddRequest);
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            ClientId = id;
            _ = LoadClient();
            TerminationAddRequest.ClientId = ClientId;
        }
    }

}
