using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client.Models;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;
public class ClientUpdateViewModel : ViewModel , IParameterReceiver
{
    private readonly ClientHttpClient _httpClient;
    public ICommand UpdateClientCommand { get; }
    public ICommand LoadClientCommand { get; }
    public ICommand CancelCommand { get; }

    private ClientEditFormModel _clientEditFormModel = new();
    public ClientEditFormModel ClientEditFormModel 
        { get { return _clientEditFormModel; } 
        set 
        {
            if( _clientEditFormModel != value)
            {
                _clientEditFormModel = value; 
                OnPropertyChanged();
            }
        } 
    }
    public INavigationService Navigation { get; }
    
    public Guid ClientId { get; set; }
    public SidebarViewModel SidebarView { get; set; }

    private async Task LoadClientAsync(Guid clientId)
    {
        Result<ClientEditResponse> result = await _httpClient.GetClientForEditByClientIdAsync(clientId);
        if(result.IsSuccess)
        {
            ClientEditResponse clientEditResponse = result.Value!;
            ClientEditFormModel = new ClientEditFormModel()
            {
                LastName = clientEditResponse.LastName,
                Street = clientEditResponse.Street,
                City = clientEditResponse.City,
                PhoneNumber = clientEditResponse.PhoneNumber
            };
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}", "Error during loading", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task UpdateClientAsync(object arg)
    {
        ClientUpdateRequest clientUpdateRequest = new ClientUpdateRequest()
        {
            City = ClientEditFormModel.City,
            LastName = ClientEditFormModel.LastName,
            PhoneNumber = ClientEditFormModel.PhoneNumber,
            Street = ClientEditFormModel.Street
        };

        Result<ClientInfoResponse> result = await _httpClient.PutClientAsync(clientUpdateRequest, ClientId);
        if (result.IsSuccess)
        {
            MessageBox.Show($"Client {result.Value!.FullName} is already edited!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Navigation.NavigateTo<ClientViewModel>();
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}", "Error during edition", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            ClientId = clientId;
        }
    }
    public ClientUpdateViewModel(SidebarViewModel sidebarViewModel, INavigationService navigation, ClientHttpClient httpClient) 
    {
        Navigation = navigation;
        SidebarView = sidebarViewModel;
        _httpClient = httpClient;
        UpdateClientCommand = new AsyncRelayCommand(UpdateClientAsync, item => CanUpdateClient());
        ClientEditFormModel.ErrorsChanged += (_, __) => ((AsyncRelayCommand)UpdateClientCommand).RaiseCanExecuteChanged();
        LoadClientCommand = new AsyncRelayCommand(item => LoadClientAsync(ClientId), item => true);
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<ClientViewModel>(), item => true);
    }

    private bool CanUpdateClient()
    {
       return !ClientEditFormModel.HasErrors && ClientEditFormModel.IsFormComplete;
    }
}
