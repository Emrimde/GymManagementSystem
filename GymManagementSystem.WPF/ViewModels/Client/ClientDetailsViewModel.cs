using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ClientMembership;
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
    Client?.CanClientAddMembership ?? false
        ? "No membership"
        : $"{Client?.ClientMembership?.Membership?.Name} {Client?.ClientMembership?.Membership?.MembershipType}";


    private INavigationService _navigation;
    public ICommand CreateNewTerminationCommand { get; }
    public ICommand OpenAddClientMembershipViewCommand { get; }
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private readonly TerminationHttpClient _httpClient;
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

    private ClientMembershipResponse _clientMembership;

    public ClientMembershipResponse ClientMembership
    {
        get { return _clientMembership; }
        set
        {
            _clientMembership = value;
            OnPropertyChanged();
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

    public ClientDetailsViewModel(SidebarViewModel sidebarViewModel, INavigationService navigation, TerminationHttpClient httpClient, ClientHttpClient clientHttpClient)
    {
        _clienthttpClient = clientHttpClient;
        _httpClient = httpClient;
        Navigation = navigation;
        Client = new ClientDetailsResponse();
        TerminationAddRequest = new TerminationAddRequest();

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
            //TerminationAddRequest.ContractId = Client?.ClientMembership?.ContractId ?? Guid.Empty;
        }
    }

}
