using System.Windows;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;
public class ClientMembershipDetailsViewModel : ViewModel, IParameterReceiver
{
    private readonly ClientMembershipHttpClient _clientMembershipHttpClient;
    private ClientMembershipDetailsResponse _clientMembership;

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

    public ClientMembershipDetailsViewModel(ClientMembershipHttpClient clientMembershipHttpClient, SidebarViewModel sidebarView)
    {
        _clientMembershipHttpClient = clientMembershipHttpClient;
        ClientMembership = new ClientMembershipDetailsResponse();
        SidebarView = sidebarView;
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            _ = LoadClientMembership(id);
        }
    }

    private async Task LoadClientMembership(Guid id)
    {
        Result<ClientMembershipDetailsResponse> result = await _clientMembershipHttpClient.GetClientMembershipDetailsAsync(id);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
            return;
        }
        ClientMembership = result.Value!;
    }
}
