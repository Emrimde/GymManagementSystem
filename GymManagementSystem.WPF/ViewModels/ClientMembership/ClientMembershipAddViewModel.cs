using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Contract;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;

public class ClientMembershipAddViewModel : ViewModel, IParameterReceiver
{
    private ClientMembershipAddRequest _clientMembershipAddRequest;

    public ClientMembershipAddRequest ClientMembershipAddRequest
    {
        get { return _clientMembershipAddRequest; }
        set { _clientMembershipAddRequest = value; OnPropertyChanged(); }
    }
    private ClientDetailsResponse _client;

    public ClientDetailsResponse Client
    {
        get { return _client; }
        set { _client = value;  OnPropertyChanged(); }
    }

    public SidebarViewModel SidebarView { get; set; }
    private INavigationService _navigation;

    private ObservableCollection<MembershipResponse> _membershipsComboBox;
    public ObservableCollection<MembershipResponse> MembershipsComboBox
    {
        get => _membershipsComboBox;
        set
        {
            if (_membershipsComboBox != value)
            {
                _membershipsComboBox = value;
                OnPropertyChanged();
            }
        }
    }
    public ICommand AddClientMembershipCommand { get; }
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private MembershipResponse _selectedMembership;
    public MembershipResponse SelectedMembership
    {
        get => _selectedMembership;
        set
        {
            if (_selectedMembership != value)
            {
                _selectedMembership = value;
                OnPropertyChanged(nameof(SelectedMembership));
                ClientMembershipAddRequest.MembershipId = _selectedMembership.Id;
            }
        }
    }
   
    private readonly ClientMembershipHttpClient _httpClient;
    private readonly ClientHttpClient _clientHttpClient;
    private readonly MembershipHttpClient _membershipHttpClient;

    public ClientMembershipAddViewModel(SidebarViewModel sidebarView, INavigationService navigation, ClientMembershipHttpClient httpClient, MembershipHttpClient membershipHttpClient, ClientHttpClient clientHttpClient)
    {
        _clientHttpClient = clientHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        _httpClient = httpClient;
        Client = new ClientDetailsResponse();
        ClientMembershipAddRequest = new ClientMembershipAddRequest() { StartDate = DateTime.UtcNow};
        _membershipHttpClient = membershipHttpClient;
        MembershipsComboBox = new ObservableCollection<MembershipResponse>();
        AddClientMembershipCommand = new AsyncRelayCommand(item => AddClientMembershipAsync(), item => true);
        _ = LoadMemberships();
    }

    private async Task AddClientMembershipAsync()
    {
        Result<ClientMembershipInfoResponse> result = await _httpClient.PostClientMembershipAsync(_clientMembershipAddRequest);
        if (result.IsSuccess) 
        {
            Navigation.NavigateTo<ContractDetailsViewModel>(result.Value!.ContractId);
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}");
        }

    }

    private async Task LoadMemberships()
    {
        MembershipsComboBox = await _membershipHttpClient.GetAllMembershipsAsync();
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
           
            _ = LoadClients(id);
            _clientMembershipAddRequest.ClientId = id;
        }
    }

    private async Task LoadClients(Guid id)
    {
        Result<ClientDetailsResponse> result = await _clientHttpClient.GetClientById(id);
        if (result.IsSuccess)
        {
            Client = result.Value!;
        }
        else
        {
            MessageBox.Show("Fail to load single client");
        }
    }
}
