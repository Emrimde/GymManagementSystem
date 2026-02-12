using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Visit;
public class VisitViewModel : ViewModel, IParameterReceiver
{
    private readonly VisitHttpClient _visitHttpClient;
    private readonly ClientHttpClient _clientHttpClient;
    public SidebarViewModel SidebarView { get; }

    public ICommand ReturnCommand { get; set; }
    public ICommand DeleteVisitCommand { get; set; }
    public ICommand LoadVisitViewDataCommand { get; set; }
    private ClientInfoResponse _clientName = new();

    public ClientInfoResponse ClientName
    {
        get { return _clientName; }
        set { _clientName = value; OnPropertyChanged(); }
    }

    public Guid ClientId { get; set; }

    public INavigationService Navigation { get; set; }

    private ObservableCollection<VisitResponse> _visits = new();

    public ObservableCollection<VisitResponse> Visits
    {
        get { return _visits; }
        set { _visits = value; OnPropertyChanged(); }
    }


    public VisitViewModel(VisitHttpClient visitHttpClient, INavigationService navigation, SidebarViewModel sidebarViewModel, ClientHttpClient clientHttpClient)
    {
        _visitHttpClient = visitHttpClient;
        Navigation = navigation;
        SidebarView = sidebarViewModel;
        _clientHttpClient = clientHttpClient;
        ReturnCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(ClientId), item => true);
        DeleteVisitCommand = new AsyncRelayCommand(item => DeleteVisitAsync(item!), item => true);
        LoadVisitViewDataCommand = new AsyncRelayCommand(item => LoadVisitViewDataAsync(), item => true);
    }

    private async Task LoadVisitViewDataAsync()
    {
        await LoadVisits(ClientId);
        await LoadClientNameAsync(ClientId);
    }

    private async Task DeleteVisitAsync(object parameter)
    {
        if (parameter is Guid visitId)
        {
            MessageBoxResult mbResult = MessageBox.Show("Are you sure to delete visit?", "Success", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (mbResult == MessageBoxResult.Yes)
            {

                Result<Unit> result = await _visitHttpClient.DeleteVisitAsync(visitId);
                if (!result.IsSuccess)
                {
                    MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    LoadVisitViewDataCommand.Execute(null);
                }
            }
            MessageBox.Show("Visit deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    private async Task LoadClientNameAsync(Guid clientId)
    {
        Result<ClientInfoResponse> result = await _clientHttpClient.GetClientNameById(clientId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        ClientName = result.Value!;
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            ClientId = clientId;
        }
    }

    private async Task LoadVisits(Guid clientId)
    {
        Result<ObservableCollection<VisitResponse>> result = await _visitHttpClient.GetAllClientVisitsAsync(clientId);
        if (result.IsSuccess)
        {
            Visits = result.Value!;
        }
        else
        {
            MessageBox.Show($"Error loading visits: {result.GetUserMessage()}");
        }
    }
}
