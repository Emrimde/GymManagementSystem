using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ClassBooking;
public class ClassBookingViewModel : ViewModel, IParameterReceiver
{
    private readonly ClassBookingHttpClient _classBookingHttpClient;
    private readonly ClientHttpClient _clientHttpClient;
    public INavigationService Navigation { get; set; }
    private Guid _clientId;
    private ClientInfoResponse _client = new();

    public ClientInfoResponse Client
    {
        get { return _client; }
        set { _client = value; OnPropertyChanged(); }
    }

    public ICommand PreviousPageCommand { get; }
    public ICommand LoadClassBookingDataCommand { get; }
    public ICommand DeleteClassBookingCommand { get; }
    public ObservableCollection<ClassBookingResponse> ClassBookings { get; set; } = new();
    public SidebarViewModel SidebarView { get; }
    public ClassBookingViewModel(ClassBookingHttpClient httpClient, INavigationService navigation, SidebarViewModel sidebarViewModel, ClientHttpClient clientHttpClient)
    {
        SidebarView = sidebarViewModel;
        _classBookingHttpClient = httpClient;
        _clientHttpClient = clientHttpClient;
        Navigation = navigation;
        PreviousPageCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(_clientId), item => true);
        LoadClassBookingDataCommand = new AsyncRelayCommand(item => LoadClassBookingDataAsync(), item => true);
        DeleteClassBookingCommand = new AsyncRelayCommand(item => DeleteClassBookingAsync(item), item => true);

    }

    private async Task DeleteClassBookingAsync(object item)
    {
        if (item is Guid _classBookingId)
        {
            MessageBoxResult mbResult = MessageBox.Show("Are you sure to cancel classBooking for this client?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mbResult == MessageBoxResult.Yes)
            {
                Result<Unit> result = await _classBookingHttpClient.DeleteClassBookingAsync(_classBookingId);

                if (!result.IsSuccess)
                {
                    MessageBox.Show($"{result.GetUserMessage()}");
                }
                await LoadClassBookingDataAsync();
            }
        }
    }

    private async Task LoadClassBookingDataAsync()
    {
        await LoadClientNameAsync();
        await LoadClassBookingsAsync(_clientId);
    }

    private async Task LoadClassBookingsAsync(Guid clientId)
    {
        ClassBookings.Clear();
        Result<ObservableCollection<ClassBookingResponse>> result = await _classBookingHttpClient.GetClassBookingsByClientId(clientId);

        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}");
        }
        foreach (ClassBookingResponse classBooking in result.Value!)
        {
            ClassBookings.Add(classBooking);
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _clientId = id;
        }
    }

    private async Task LoadClientNameAsync()
    {
        Result<ClientInfoResponse> result = await _clientHttpClient.GetClientNameById(_clientId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Client = result.Value!;
    }
}
