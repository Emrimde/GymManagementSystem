using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ClassBooking;
public class ClassBookingViewModel : ViewModel, IParameterReceiver
{
    private readonly ClassBookingHttpClient _httpClient;
    private readonly ClientHttpClient _clientHttpClient;
    public INavigationService Navigation { get; set; }
    public Guid ClientId { get; set; }
    private ClientInfoResponse _client = new();

    public ClientInfoResponse Client
    {
        get { return _client; }
        set { _client = value; OnPropertyChanged(); }
    }

    public ICommand PreviousPageCommand { get;  }
    public ObservableCollection<ClassBookingResponse> ClassBookings { get; set; }
    public SidebarViewModel SidebarView { get; }
    public ClassBookingViewModel(ClassBookingHttpClient httpClient, INavigationService navigation,SidebarViewModel sidebarViewModel, ClientHttpClient clientHttpClient)
    {
        SidebarView = sidebarViewModel;
        _httpClient = httpClient;
        _clientHttpClient = clientHttpClient;
        Navigation = navigation;
        ClassBookings = new ObservableCollection<ClassBookingResponse>();
        PreviousPageCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(ClientId), item => true);
    }

    private async Task LoadClassBookings(Guid clientId)
    {
        Result<ObservableCollection<ClassBookingResponse>> result = await _httpClient.GetClassBookingsByClientId(clientId);

        if (!result.IsSuccess) 
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        foreach (ClassBookingResponse classBooking in result.Value!)
        {
            ClassBookings.Add(classBooking);
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            ClientId = id;
            _ = LoadClientName();
            _ = LoadClassBookings(id);
        }
    }

    private async Task LoadClientName()
    {
        Client = await _clientHttpClient.GetClientNameById(ClientId);
    }
}
