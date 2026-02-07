using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ClassBooking;
public class AddClassBookingViewModel : ViewModel, IParameterReceiver
{
    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation { get; set; }
    private readonly ClassBookingHttpClient _classBookingHttpClient;
    private readonly GymClassHtppClient _gymClassHttpClient;
    private readonly ScheduledClassHttpClient _scheduledClassHttpClient;
    private readonly ClientHttpClient _clientHttpClient;
    public ClassBookingAddRequest ClassBookingRequest { get; set; }
    public ICommand AddClassBookingCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand LoadDataForAddClassBookingCommand { get; }
    public ICommand LoadScheduledClassesCommand { get; }
    public Guid ClientId { get; set; }
    private ObservableCollection<ScheduledClassComboBoxResponse> _scheduledClasses = new();

    public ObservableCollection<ScheduledClassComboBoxResponse> ScheduledClasses
    {
        get { return _scheduledClasses; }
        set { _scheduledClasses = value; OnPropertyChanged(); }
    }

    private ScheduledClassComboBoxResponse? _selectedScheduledClass;

    public ScheduledClassComboBoxResponse? SelectedScheduledClass
    {
        get => _selectedScheduledClass;
        set
        {
            _selectedScheduledClass = value;

            if (value != null)
                ClassBookingRequest.ScheduledClassId = value.ScheduledClassId;

            OnPropertyChanged();
        }
    }

    private ClientInfoResponse _client = new();

    public ClientInfoResponse Client
    {
        get { return _client; }
        set { _client = value; OnPropertyChanged(); }
    }


    private ObservableCollection<GymClassComboBoxResponse> _gymClasses = new();

    public ObservableCollection<GymClassComboBoxResponse> GymClasses
    {
        get { return _gymClasses; }
        set { _gymClasses = value; OnPropertyChanged(); }
    }
    private bool _isScheduledClassComboBoxVisible;

    public bool IsScheduledClassComboBoxVisible
    {
        get { return _isScheduledClassComboBoxVisible; }
        set { _isScheduledClassComboBoxVisible = value; OnPropertyChanged(); }
    }


    private GymClassComboBoxResponse _selectedGymClass = new();

    public GymClassComboBoxResponse SelectedGymClass
    {
        get { return _selectedGymClass; }
        set
        {
            _selectedGymClass = value;
            IsScheduledClassComboBoxVisible = true;
            ((AsyncRelayCommand)LoadScheduledClassesCommand)
           .Execute(null);
            OnPropertyChanged();
        }
    }
    public AddClassBookingViewModel(SidebarViewModel sidebarView, INavigationService navigation, ClassBookingHttpClient classBookingHttpClient, GymClassHtppClient gymClassHttpClient, ScheduledClassHttpClient scheduledClassHttpClient, ClientHttpClient clientHttpClient)
    {
        IsScheduledClassComboBoxVisible = false;
        ClassBookingRequest = new ClassBookingAddRequest();
        SidebarView = sidebarView;
        Navigation = navigation;
        LoadScheduledClassesCommand = new AsyncRelayCommand(item => LoadScheduledClassesComboBox(Client.MembershipId), item => true);
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(ClientId), item => true);
        LoadDataForAddClassBookingCommand = new AsyncRelayCommand(item => LoadAllAsync(ClientId), item => true);
        _clientHttpClient = clientHttpClient;
        _classBookingHttpClient = classBookingHttpClient;
        _gymClassHttpClient = gymClassHttpClient;
        _scheduledClassHttpClient = scheduledClassHttpClient;
        AddClassBookingCommand = new AsyncRelayCommand(item => AddClassBookingAsync(), item => true);
    }

    private async Task LoadAllAsync(Guid clientId)
    {
        await LoadGymClassesAsync();
        await LoadClientName(clientId);
    }

    private async Task AddClassBookingAsync()
    {
        Result<ClassBookingInfoResponse> result = await _classBookingHttpClient.PostClassBookingAsync(ClassBookingRequest);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}");
        }
        Navigation.NavigateTo<ClientDetailsViewModel>(ClientId);
    }

    private async Task LoadScheduledClassesComboBox(Guid? membershipId)
    {
        if (membershipId.HasValue)
        {
            Result<ObservableCollection<ScheduledClassComboBoxResponse>> result = await _scheduledClassHttpClient.GetScheduledClassesComboBox(SelectedGymClass.GymClassId, ClientId);
            if (!result.IsSuccess)
            {
                MessageBox.Show($"{result.GetUserMessage()}");
            }
            ScheduledClasses = result.Value!;
            SelectedScheduledClass = null;
        }
    }

    private async Task LoadGymClassesAsync()
    {
        Result<ObservableCollection<GymClassComboBoxResponse>> result = await _gymClassHttpClient.GetGymClassComboBoxResponses();
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage}");
        }
        GymClasses = result.Value!;

    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            ClientId = clientId;
            ClassBookingRequest.ClientId = clientId;
        }
    }

    private async Task LoadClientName(Guid clientId)
    {
        Result<ClientInfoResponse> result = await _clientHttpClient.GetClientNameById(clientId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Client = result.Value!;
    }
}
