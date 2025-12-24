using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ClassBooking;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using GymManagementSystem.WPF.ViewModels.Termination;
using GymManagementSystem.WPF.ViewModels.Visit;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;
public class ClientDetailsViewModel : ViewModel, IParameterReceiver
{
    public string ActiveMembershipName =>
    Client.IsActive
        ? $"{Client.ClientMembership?.Membership?.Name} {Client.ClientMembership?.Membership?.MembershipType}"
        : "No membership";
    private readonly VisitHttpClient _visitHttpClient;

    private INavigationService _navigation;
    public ICommand CreateNewTerminationCommand { get; }
    public ICommand OpenVisitsHistoryCommand { get; }
    public ICommand OpenAddClassBooking { get; }
    public ICommand RegisterVisitCommand { get; }
    public ICommand OpenClientMembershipsHistory { get; }
    public ICommand OpenAddClientMembershipViewCommand { get; }
    public ICommand OpenAllClientClassBookingCommand { get; }
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    public Guid ClientId { get; set; }
    private ClientDetailsResponse _client;
    public ClientDetailsResponse Client
    {
        get => _client;
        set
        {
            _client = value; OnPropertyChanged();
            OnPropertyChanged(nameof(ActiveMembershipName));
        }
    }

    private TerminationAddRequest _terminationAdd;

    public TerminationAddRequest TerminationAddRequest
    {
        get { return _terminationAdd; }
        set
        {
            _terminationAdd = value;
            OnPropertyChanged();
        }
    }


    private readonly ClientHttpClient _clienthttpClient;
    public SidebarViewModel SidebarView { get; }

    public ClientDetailsViewModel(SidebarViewModel sidebarViewModel, INavigationService navigation, ClientHttpClient clientHttpClient, VisitHttpClient visitHttpClient)
    {
        _clienthttpClient = clientHttpClient;
        Navigation = navigation;
        Client = new ClientDetailsResponse();
        TerminationAddRequest = new TerminationAddRequest();
        OpenVisitsHistoryCommand = new RelayCommand(item =>
            Navigation.NavigateTo<VisitViewModel>(ClientId), item => true);

        OpenClientMembershipsHistory = new RelayCommand(item =>
            Navigation.NavigateTo<ClientMembershipViewModel>(ClientId), item => true);
        OpenAddClassBooking = new RelayCommand(item => Navigation.NavigateTo<AddClassBookingViewModel>(Client.Id), item => true);

        RegisterVisitCommand = new AsyncRelayCommand(item => RegisterVisitAsync(), item => true);
        SidebarView = sidebarViewModel;
        CreateNewTerminationCommand = new RelayCommand(item =>

         OpenCreateNewTermination()
        , item => true);
        OpenAllClientClassBookingCommand = new RelayCommand(item => Navigation.NavigateTo<ClassBookingViewModel>(ClientId), item => true);


        OpenAddClientMembershipViewCommand = new RelayCommand(item =>
        {
            if (item is Guid id)
            {
                Navigation.NavigateTo<ClientMembershipAddViewModel>(item);
            }
            else
            {
                MessageBox.Show("Fail");
            }

        }
           , item => true);
        _visitHttpClient = visitHttpClient;
    }

    private async Task RegisterVisitAsync()
    {
        if (!Client.IsActive)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Is client paid for single entry?.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MessageBox.Show("Visit registered for single entry.");

            }
            else
            {
                return;
            }

        }
        Result<Unit> result = await _visitHttpClient.RegisterVisitAsync(ClientId);
        if(result.IsSuccess)
        {
            

            await LoadClient();
        }
        else
        {
            MessageBox.Show($"Error registering visit: {result.ErrorMessage}");
        }
    }

    private async Task LoadClient()
    {
        Result<ClientDetailsResponse> result = await _clienthttpClient.GetClientById(ClientId);
        if (result.IsSuccess)
        {
            Client = result.Value!;
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}");
        }
    }

    private void OpenCreateNewTermination()
    {
        Navigation.NavigateTo<TerminationAddViewModel>(ClientId);
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            ClientId = id;
            _ = LoadClient();
        }
    }

}
