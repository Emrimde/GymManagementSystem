using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;
public class ClientUpdateViewModel : ViewModel , IParameterReceiver
{
    private INavigationService _navigation;
    private ClientUpdateRequest _clientUpdateRequest;
    private readonly ClientHttpClient _httpClient;
    public ICommand UpdateClientCommand { get; }

    public ClientUpdateRequest ClientUpdateRequest { get { return _clientUpdateRequest; } 
        set 
        {
            if( _clientUpdateRequest != value)
            {
                _clientUpdateRequest = value; 
                OnPropertyChanged();
            }
        } 
    }
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }
    public Guid ClientId { get; private set; }
    public SidebarViewModel SidebarView { get; set; }
    public ClientUpdateViewModel(SidebarViewModel sidebarViewModel, INavigationService navigation, ClientHttpClient httpClient) 
    {
        Navigation = navigation;
        SidebarView = sidebarViewModel;
        ClientUpdateRequest = new ClientUpdateRequest();
        _httpClient = httpClient;
        UpdateClientCommand = new AsyncRelayCommand(UpdateClientAsync, item => true);
    }

    private async Task UpdateClientAsync(object arg)
    {
        Result<ClientResponse> result = await _httpClient.PutClientAsync(ClientUpdateRequest, ClientId);
        if (result.IsSuccess)
        {
            MessageBox.Show($"Client {result.Value!.FirstName} {result.Value.LastName} is already edited!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Navigation.NavigateTo<ClientViewModel>();
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}", "Error during edition", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is ClientResponse client)
        {
            
            //ClientUpdateRequest.Id = client.Id;
            ClientId = client.Id;
            ClientUpdateRequest.FirstName = client.FirstName;
            ClientUpdateRequest.LastName = client.LastName;
            ClientUpdateRequest.Email = client.Email;
            ClientUpdateRequest.PhoneNumber = client.PhoneNumber;
            ClientUpdateRequest.DateOfBirth = client.DateOfBirth;
            ClientUpdateRequest.Street = client.Street;
            ClientUpdateRequest.City = client.City;
        }
    }
}
