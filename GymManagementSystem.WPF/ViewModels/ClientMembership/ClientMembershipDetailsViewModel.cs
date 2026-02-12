using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;
public class ClientMembershipDetailsViewModel : ViewModel, IParameterReceiver
{
    public INavigationService Navigation { get; set; }

    private readonly ClientMembershipHttpClient _clientMembershipHttpClient;
    private ClientMembershipDetailsResponse _clientMembership = new();

    public ClientMembershipDetailsResponse ClientMembership
    {
        get { return _clientMembership; }
        set
        {
            _clientMembership = value;
            OnPropertyChanged();
        }
    }
    public SidebarViewModel SidebarView { get; set; }

    public ClientMembershipDetailsViewModel(ClientMembershipHttpClient clientMembershipHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        Navigation = navigation;
        _clientMembershipHttpClient = clientMembershipHttpClient;
        ClientMembership = new ClientMembershipDetailsResponse();
        SidebarView = sidebarView;
        OpenClientMembershipsHistoryCommand = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipViewModel>(item!), item => true);
        LoadClientMembershipCommand = new AsyncRelayCommand(item => LoadClientMembershipAsync(), item => true);
       
    }

    private Guid _id;

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _id = id;
        }
    }

    public ICommand OpenClientMembershipsHistoryCommand { get; }
    public ICommand LoadClientMembershipCommand { get; }

    private async Task LoadClientMembershipAsync()
    {
        Result<ClientMembershipDetailsResponse> result = await _clientMembershipHttpClient.GetClientMembershipDetailsAsync(_id);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}");
            return;
        }
        ClientMembership = result.Value!;
    }
}
