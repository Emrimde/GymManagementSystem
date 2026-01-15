using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;
public class ClientUpdateViewModel : ViewModel , IParameterReceiver , IDataErrorInfo
{
    private INavigationService _navigation;
    private readonly ClientHttpClient _httpClient;
    public ICommand UpdateClientCommand { get; }
    public ICommand LoadClientCommand { get; }
    public ICommand CancelCommand { get; }

    private ClientEditResponse _clientEditResponse;
    public ClientEditResponse ClientEditResponse 
        { get { return _clientEditResponse; } 
        set 
        {
            if( _clientEditResponse != value)
            {
                _clientEditResponse = value; 
                OnPropertyChanged();
            }
        } 
    }
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }
    public Guid ClientId { get; set; }
    public SidebarViewModel SidebarView { get; set; }

    public string Error => null;

    public string this[string columnName]
    {
        get
        {
            if (ClientEditResponse == null)
                return null;

            switch (columnName)
            {
                case nameof(ClientEditResponse.LastName):
                    if (string.IsNullOrWhiteSpace(ClientEditResponse.LastName))
                        return "Last name is required.";
                    if (ClientEditResponse.LastName.Length > 50)
                        return "Last name cannot exceed 50 characters.";
                    break;

                case nameof(ClientEditResponse.PhoneNumber):
                    if (string.IsNullOrWhiteSpace(ClientEditResponse.PhoneNumber))
                        return "Phone number is required.";
                    if (ClientEditResponse.PhoneNumber.Length > 15)
                        return "Phone number cannot exceed 15 characters.";
                    if (!System.Text.RegularExpressions.Regex.IsMatch(
                            ClientEditResponse.PhoneNumber, @"^\+?[1-9]\d{1,14}$"))
                        return "A valid phone number is required.";
                    break;

                case nameof(ClientEditResponse.Street):
                    if (string.IsNullOrWhiteSpace(ClientEditResponse.Street))
                        return "Street is required.";
                    if (ClientEditResponse.Street.Length > 100)
                        return "Street cannot exceed 100 characters.";
                    break;

                case nameof(ClientEditResponse.City):
                    if (string.IsNullOrWhiteSpace(ClientEditResponse.City))
                        return "City is required.";
                    if (ClientEditResponse.City.Length > 100)
                        return "City cannot exceed 100 characters.";
                    break;
            }

            return null;
        }
    }


    public ClientUpdateViewModel(SidebarViewModel sidebarViewModel, INavigationService navigation, ClientHttpClient httpClient) 
    {
        Navigation = navigation;
        SidebarView = sidebarViewModel;
        ClientEditResponse = new ClientEditResponse();
        _httpClient = httpClient;
        UpdateClientCommand = new AsyncRelayCommand(UpdateClientAsync, item => true);
        LoadClientCommand = new AsyncRelayCommand(item => LoadClientAsync(ClientId), item => true);
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<ClientViewModel>(), item => true);
    }

    private async Task LoadClientAsync(Guid clientId)
    {
        Result<ClientEditResponse> result = await _httpClient.GetClientForEditByClientIdAsync(clientId);
        if(result.IsSuccess)
        {
            ClientEditResponse = result.Value!;
            OnPropertyChanged(nameof(LastName));
            OnPropertyChanged(nameof(Street));
            OnPropertyChanged(nameof(City));
            OnPropertyChanged(nameof(PhoneNumber));
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
            City = ClientEditResponse.City,
            LastName = ClientEditResponse.LastName,
            PhoneNumber = ClientEditResponse.PhoneNumber,
            Street = ClientEditResponse.Street
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

    public string LastName
    {
        get => ClientEditResponse.LastName;
        set
        {
            ClientEditResponse.LastName = value;
            OnPropertyChanged();
        }
    }
    public string Street
    {
        get => ClientEditResponse.Street;
        set
        {
            ClientEditResponse.Street = value;
            OnPropertyChanged();
        }
    }
    public string City
    {
        get => ClientEditResponse.City;
        set
        {
            ClientEditResponse.City = value;
            OnPropertyChanged();
        }
    }
    public string PhoneNumber
    {
        get => ClientEditResponse.PhoneNumber;
        set
        {
            ClientEditResponse.PhoneNumber = value;
            OnPropertyChanged();
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
