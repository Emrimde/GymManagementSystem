using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;

public class ClientMembershipViewModel : ViewModel
{
    private readonly ClientMembershipHttpClient _httpClient;
    private ObservableCollection<ClientMembershipResponse> _clientMemberships;

    public ObservableCollection<ClientMembershipResponse> ClientMemberships
    {

        get { return _clientMemberships; }
        set { _clientMemberships = value; OnPropertyChanged(); }
    }

    public INavigationService _navigation;

    public ClientMembershipViewModel(ClientMembershipHttpClient httpClient, INavigationService navigation, SidebarViewModel sidebarView)
    {
        _httpClient = httpClient;
        Navigation = navigation;
        SidebarView = sidebarView;
        ClientMemberships = new ObservableCollection<ClientMembershipResponse>();
        _ = LoadClientMemberships();
    }

    private async Task LoadClientMemberships()
    {
        ClientMemberships = await _httpClient.GetClientMembershipsAsync();
    }

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }
    public SidebarViewModel SidebarView { get; }

    

}
