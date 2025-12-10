using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;

public class ClientAddViewModel : ViewModel
{
    private readonly ClientHttpClient _httpClient;
    public SidebarViewModel SidebarView { get; set; }
    public ICommand AddClientCommand { get; }

    private ClientAddRequest _clientAddRequest;
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
    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }

    public ClientAddViewModel(SidebarViewModel sidebarView, ClientHttpClient httpClient, INavigationService navigation, MembershipHttpClient membershipHttpClient)
    {
        _httpClient = httpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        ClientAddRequest = new ClientAddRequest()
        {
            DateOfBirth = DateTime.SpecifyKind(new DateTime(2000, 1, 1), DateTimeKind.Utc)
        };
        AddClientCommand = new AsyncRelayCommand(item => AddClientAsync(), item => true);

    }
    private async Task AddClientAsync()
    {

        ClientAgeValidationRequest validationRequest = new ClientAgeValidationRequest
        {
            DateOfBirth = ClientAddRequest.DateOfBirth
        };
       

        var validationResult = await _httpClient.ValidateClientAgeAsync(validationRequest);

        if (!validationResult.IsSuccess)
        {
            MessageBox.Show(validationResult.ErrorMessage);
            return;
        }

        if (validationResult.Value!.Age < 18 && validationResult.Value!.Age > 13)
        {
           
            MessageBoxResult result = MessageBox.Show(
                "Client is under 18. Does client has parent consent?",
                "Age Restriction",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
                return;
        }
        else
        {
            MessageBox.Show("Client must be at least 14 years old to register.", "Age Restriction", MessageBoxButton.OK, MessageBoxImage.Error);
            Navigation.NavigateTo<ClientViewModel>();
            return;
        }


            Result<ClientInfoResponse> addResult = await _httpClient.PostClientAsync(ClientAddRequest);

        if (!addResult.IsSuccess)
        {
            MessageBox.Show(addResult.ErrorMessage);
            return;
        }

        Navigation.NavigateTo<ClientViewModel>();
    }

}
