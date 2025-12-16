using System.Windows;
using System.Windows.Input;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;
public class ClientMembershipDetailsViewModel : ViewModel, IParameterReceiver
{
   

    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

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

    public ClientMembershipDetailsViewModel(ClientMembershipHttpClient clientMembershipHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        Navigation = navigation;
        _clientMembershipHttpClient = clientMembershipHttpClient;
        ClientMembership = new ClientMembershipDetailsResponse();
        SidebarView = sidebarView;
        OpenClientMembershipsHistoryCommand = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipViewModel>(item), item => true);
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            
            _ = LoadClientMembership(id);
        }
    }

    public ICommand OpenClientMembershipsHistoryCommand { get; }

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
