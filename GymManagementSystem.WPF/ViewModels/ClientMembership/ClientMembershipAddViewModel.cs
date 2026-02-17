using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.ClientMembership.Models;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.PdfGenerators;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;

public class ClientMembershipAddViewModel : ViewModel, IParameterReceiver
{
    private readonly ClientMembershipHttpClient _httpClient;
    private readonly ClientHttpClient _clientHttpClient;
    private readonly MembershipHttpClient _membershipHttpClient;

    private ClientMembershipAddRequest _clientMembershipAddRequest = new();
    public Guid ClientId { get; set; }
    public ClientMembershipAddRequest ClientMembershipAddRequest
    {
        get { return _clientMembershipAddRequest; }
        set { _clientMembershipAddRequest = value; OnPropertyChanged(); }
    }
    private ClientInfoResponse _client = new();

    public ClientInfoResponse Client
    {
        get { return _client; }
        set { _client = value; OnPropertyChanged(); }
    }

    public SidebarViewModel SidebarView { get; set; }

    private ObservableCollection<MembershipSelectItem> _membershipSelectItem = new();
    public ObservableCollection<MembershipSelectItem> MembershipSelectItem
    {
        get => _membershipSelectItem;
        set
        {
            if (_membershipSelectItem != value)
            {
                _membershipSelectItem = value;
                OnPropertyChanged();
            }
        }
    }
    public ICommand AddClientMembershipCommand { get; }
    public ICommand LoadClientMembershipViewData { get; }
    public ICommand CancelCommand { get; }

    public INavigationService Navigation { get; set; }


    private MembershipSelectItem _selectedMembership = new();
    public MembershipSelectItem SelectedMembership
    {
        get => _selectedMembership;
        set
        {
            if (_selectedMembership != value)
            {
                _selectedMembership = value;
                OnPropertyChanged(nameof(SelectedMembership));
                ClientMembershipAddRequest.MembershipId = _selectedMembership.Id;
                ((AsyncRelayCommand)AddClientMembershipCommand)
                .RaiseCanExecuteChanged();
            }
        }
    }


    public ClientMembershipAddViewModel(SidebarViewModel sidebarView, INavigationService navigation, ClientMembershipHttpClient httpClient, MembershipHttpClient membershipHttpClient, ClientHttpClient clientHttpClient)
    {
        _clientHttpClient = clientHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        _httpClient = httpClient;
        _membershipHttpClient = membershipHttpClient;
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(Client.Id), item => true);
        AddClientMembershipCommand = new AsyncRelayCommand(item => AddClientMembershipAsync(), item => CanAddClientMembership());
        LoadClientMembershipViewData = new AsyncRelayCommand(item => LoadClientMembershipViewDataAsync(), item => true);
    }

    private bool CanAddClientMembership()
    {
        return ClientId != Guid.Empty && _clientMembershipAddRequest.MembershipId != Guid.Empty;
    }

    private async Task LoadClientMembershipViewDataAsync()
    {
        await LoadMemberships();
        await LoadClientName();
    }

    private async Task AddClientMembershipAsync()
    {
        Result<ClientMembershipContractPreviewResponse> contractDetails = await _httpClient.GetContractPreviewDetailsAsync(_clientMembershipAddRequest.ClientId, _clientMembershipAddRequest.MembershipId);
        ContractGenerator.GenerateClientMembershipContractPdf(contractDetails.Value!);
        MessageBoxResult messageBoxResult = MessageBox.Show("Is client signed a contract?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (messageBoxResult == MessageBoxResult.Yes)
        {
            Result<ClientMembershipInfoResponse> result = await _httpClient.PostClientMembershipAsync(_clientMembershipAddRequest);
            if (!result.IsSuccess)
            {
                MessageBox.Show($"Error: {result.GetUserMessage()}");
                return;
            }
        }
        Navigation.NavigateTo<ClientDetailsViewModel>(ClientId);

    }

    private async Task LoadMemberships()
    {
        Result<ObservableCollection<MembershipResponse>> result = await _membershipHttpClient.GetAllMembershipsAsync();
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            MembershipSelectItem = new ObservableCollection<MembershipSelectItem>(
                result.Value!.Select(item => new MembershipSelectItem
                {
                    Id = item.Id,
                    Name = item.Name + " " + item.MembershipType,
                }));
        }
    }
    private async Task LoadClientName()
    {
        Result<ClientInfoResponse> result = await _clientHttpClient.GetClientNameById(ClientId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Client = result.Value!;
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            ClientId = clientId;
            _clientMembershipAddRequest.ClientId = clientId;
        }
    }
}
