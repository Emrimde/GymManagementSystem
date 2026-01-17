using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client.Models;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;

public class ClientAddViewModel : ViewModel
{
    private readonly ClientHttpClient _httpClient;
    public SidebarViewModel SidebarView { get; set; }
    public ICommand AddClientCommand { get; }

    private ClientAddFormModel _clientAddRequest = new();
    public ClientAddFormModel ClientAddRequest
    {
        get { return _clientAddRequest; }

        set
        {
            if (_clientAddRequest != value)
            {
                _clientAddRequest = value;
                OnPropertyChanged();
                ((AsyncRelayCommand)AddClientCommand).RaiseCanExecuteChanged();
            }
        }
    }
    public INavigationService Navigation { get; }

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
        else if (validationResult.Value!.Age <= 13)
        {
            MessageBox.Show("Client must be at least 14 years old to register.", "Age Restriction", MessageBoxButton.OK, MessageBoxImage.Error);
            Navigation.NavigateTo<ClientViewModel>();
            return;
        }
      
        ClientAddRequest request = new ClientAddRequest
        {
            FirstName = ClientAddRequest.FirstName,
            LastName = ClientAddRequest.LastName,
            Email = ClientAddRequest.Email,
            PhoneNumber = ClientAddRequest.PhoneNumber,
            DateOfBirth = ClientAddRequest.DateOfBirth,
            Street = ClientAddRequest.Street,
            City = ClientAddRequest.City
        };

        Result<ClientInfoResponse> addResult = await _httpClient.PostClientAsync(request);

        if (!addResult.IsSuccess)
        {
            MessageBox.Show(addResult.ErrorMessage);
            return;
        }

        Navigation.NavigateTo<ClientDetailsViewModel>(addResult.Value!.Id);
    }
    public ClientAddViewModel(SidebarViewModel sidebarView, ClientHttpClient httpClient, INavigationService navigation)
    {
        _httpClient = httpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        AddClientCommand = new AsyncRelayCommand(item => AddClientAsync(),item => CanAddClient());
        ClientAddRequest.ErrorsChanged += (_, __) => ((AsyncRelayCommand)AddClientCommand).RaiseCanExecuteChanged();
    }

    private bool CanAddClient()
    {
        return !ClientAddRequest.HasErrors && ClientAddRequest.IsFormComplete;
    }
}
