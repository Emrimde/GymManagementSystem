using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;

public class ClientAddViewModel : ViewModel
{
    private readonly ClientHttpClient _httpClient;
    private readonly MembershipHttpClient _membershipHttpClient;

    private ObservableCollection<MembershipResponse> _memberships;

    public ObservableCollection<MembershipResponse> Memberships
    {
        get { return _memberships; }
        set
        {
            if (_memberships != value)
            {
                _memberships = value;
                OnPropertyChanged();
            }
        }
    }
    private Guid _selectedMembership;
    public Guid SelectedMembershipId { get { return _selectedMembership; } set { _selectedMembership = value; OnPropertyChanged(); } }
    public SidebarViewModel SidebarView { get; set; }
    public ICommand AddClientCommand { get; }
    private ClientAddRequest _clientAddRequest;
    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }
    public ClientAddRequest ClientAddRequest
    {
        get { return _clientAddRequest; }

        set
        {
            if (_clientAddRequest != value)
            {
                _clientAddRequest = value;
                OnPropertyChanged();
            }
        }
    }

    public ClientAddViewModel(SidebarViewModel sidebarView, ClientHttpClient httpClient, INavigationService navigation, MembershipHttpClient membershipHttpClient)
    {
        _membershipHttpClient = membershipHttpClient;
        Memberships = new ObservableCollection<MembershipResponse>();
        _ = LoadMembershipsAsync();
        _httpClient = httpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        ClientAddRequest = new ClientAddRequest()
        {
            DateOfBirth = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc)
        };
        AddClientCommand = new AsyncRelayCommand(AddClientAsync, item => true);

    }

    private async Task LoadMembershipsAsync()
    {
        Memberships = await _membershipHttpClient.GetAllMembershipsAsync();
    }

    private async Task AddClientAsync(object arg)
    {  
        Result<ClientResponse> result = await _httpClient.PostClientAsync(ClientAddRequest);
        if (result.IsSuccess)
        {
            string firstName = result.Value!.FirstName;
            string lastName = result.Value!.LastName;
            MessageBox.Show($"Success, client {firstName} {lastName} is already created",null,MessageBoxButton.OK);
            Navigation.NavigateTo<ClientViewModel>();
        }
        else
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
    }
}
