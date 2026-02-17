using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ClassBooking;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using GymManagementSystem.WPF.ViewModels.PersonalBooking;
using GymManagementSystem.WPF.ViewModels.Termination;
using GymManagementSystem.WPF.ViewModels.Visit;
using GymManagementSystem.WPF.Views.Visit.CustomDialogs;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;
public class ClientDetailsViewModel : ViewModel, IParameterReceiver
{
    public string ActiveMembershipName =>
    Client.IsActive
        ? $"{Client.ClientMembershipName}"
        : "No membership";

    private readonly VisitHttpClient _visitHttpClient;

    public ICommand CreateNewTerminationCommand { get; }
    public ICommand OpenVisitsHistoryCommand { get; }
    public ICommand OpenAddClassBooking { get; }
    public ICommand RegisterVisitCommand { get; }
    public ICommand OpenClientMembershipsHistory { get; }
    public ICommand OpenAddClientMembershipViewCommand { get; }
    public ICommand OpenAllClientClassBookingCommand { get; }
    public ICommand OpenPersonalTrainingAddViewCommand { get; }
    public ICommand LoadClientDetailsCommand { get; }
    public ICommand OpenPersonalBookingsViewCommand { get; }
    public INavigationService Navigation { get; }

    public Guid ClientId { get; set; }
    private ClientDetailsResponse _client = new();
    public ClientDetailsResponse Client
    {
        get => _client;
        set
        {
            _client = value; OnPropertyChanged();
            OnPropertyChanged(nameof(ActiveMembershipName));
        }
    }

    private readonly ClientHttpClient _clienthttpClient;
    public SidebarViewModel SidebarView { get; }

    public ClientDetailsViewModel(SidebarViewModel sidebarViewModel, INavigationService navigation, ClientHttpClient clientHttpClient, VisitHttpClient visitHttpClient)
    {
        _clienthttpClient = clientHttpClient;
        _visitHttpClient = visitHttpClient;
        Navigation = navigation;
        OpenVisitsHistoryCommand = new RelayCommand(item =>
            Navigation.NavigateTo<VisitViewModel>(ClientId), item => true);

        OpenClientMembershipsHistory = new RelayCommand(item =>
            Navigation.NavigateTo<ClientMembershipViewModel>(ClientId), item => true);
        OpenAddClassBooking = new RelayCommand(item => Navigation.NavigateTo<AddClassBookingViewModel>(Client.Id), item => true);

        RegisterVisitCommand = new AsyncRelayCommand(item => RegisterVisitAsync(), item => true);
        SidebarView = sidebarViewModel;
        CreateNewTerminationCommand = new RelayCommand(item => OpenCreateNewTermination(), item => true);
        OpenAllClientClassBookingCommand = new RelayCommand(item => Navigation.NavigateTo<ClassBookingViewModel>(ClientId), item => true);

        OpenPersonalTrainingAddViewCommand = new RelayCommand(item => Navigation.NavigateTo<PersonalBookingAddViewModel>(Client.Id), item => true);
        OpenAddClientMembershipViewCommand = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipAddViewModel>(Client.Id), item => true);
        OpenPersonalBookingsViewCommand = new RelayCommand(item => Navigation.NavigateTo<PersonalBookingViewModel>(Client.Id), item => true);
        LoadClientDetailsCommand = new AsyncRelayCommand(item => LoadClientAsync(), item => true);

    }

    private async Task RegisterVisitAsync()
    {
        if (!Client.IsActive)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Is client paid for single entry?.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                MessageBox.Show("Visit registered for single entry.");
                Result<Unit> result = await _visitHttpClient.RegisterVisitAsync(ClientId, null);
                if (result.IsSuccess)
                {
                    await LoadClientAsync();
                }
                else
                {
                    MessageBox.Show($"Error registering visit: {result.GetUserMessage()}");
                }
                return;
            }

        }

        MessageBoxResult mbResult = MessageBox.Show("Do client wants bring friend for free?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

        string? GuestName = null;
        if (mbResult == MessageBoxResult.Yes)
        {
            var dialog = new FreeFriendArrivalDialog();
            dialog.Owner = Application.Current.MainWindow;

            if (dialog.ShowDialog() == true)
            {
                GuestName = dialog.InputText;
            }
            Result<Unit> result = await _visitHttpClient.RegisterVisitAsync(ClientId, GuestName);
            if (result.IsSuccess)
            {
                await LoadClientAsync();
            }
            else
            {
                MessageBox.Show($"Error registering visit: {result.GetUserMessage()}");
            }
            return;

        }
      
    }

       
    

    private async Task LoadClientAsync()
    {
        Result<ClientDetailsResponse> result = await _clienthttpClient.GetClientById(ClientId);
        if (result.IsSuccess)
        {
            Client = result.Value!;
        }
        else
        {
            MessageBox.Show($"Error: {result.GetUserMessage()}");
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
            LoadClientDetailsCommand.Execute(null);
        }
    }

}
