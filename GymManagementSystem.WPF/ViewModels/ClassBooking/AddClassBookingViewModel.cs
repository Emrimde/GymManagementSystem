using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.GymClass;
using GymManagementSystem.Core.DTO.ScheduledClass;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
    public Guid ClientId { get; set; }
    private ObservableCollection<ScheduledClassComboBoxResponse> _scheduledClasses;

    public ObservableCollection<ScheduledClassComboBoxResponse> ScheduledClasses
    {
        get { return _scheduledClasses; }
        set { _scheduledClasses = value; OnPropertyChanged(); }
    }

    private ScheduledClassComboBoxResponse? _selectedScheduledClass;

    public ScheduledClassComboBoxResponse? SelectedScheduledClass
    {
        get { return _selectedScheduledClass; }
        set
        {
            if (value == null)
            {
                _selectedScheduledClass = null;
                return;
                    }
            if (value.ScheduledClassId != Guid.Empty)
            {

                _selectedScheduledClass = value;

                ClassBookingRequest.ScheduledClassId = _selectedScheduledClass.ScheduledClassId;
            }
        }
    }
    private ClientInfoResponse _client;

    public ClientInfoResponse Client
    {
        get { return _client; }
        set { _client = value; OnPropertyChanged(); }
    }


    private ObservableCollection<GymClassComboBoxResponse> _gymClasses;

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


    private GymClassComboBoxResponse _selectedGymClass;

    public GymClassComboBoxResponse SelectedGymClass
    {
        get { return _selectedGymClass; }
        set
        {
            _selectedGymClass = value;
            IsScheduledClassComboBoxVisible = true;
            _ = LoadScheduledClassesComboBox();
            OnPropertyChanged();
        }
    }
    public AddClassBookingViewModel(SidebarViewModel sidebarView, INavigationService navigation, ClassBookingHttpClient classBookingHttpClient, GymClassHtppClient gymClassHttpClient, ScheduledClassHttpClient scheduledClassHttpClient, ClientHttpClient clientHttpClient)
    {
        IsScheduledClassComboBoxVisible = false;
        //SelectedGymClass = new GymClassComboBoxResponse();
        //SelectedScheduledClass = new ScheduledClassComboBoxResponse();
        ClassBookingRequest = new ClassBookingAddRequest();
        ScheduledClasses = new ObservableCollection<ScheduledClassComboBoxResponse>();
        GymClasses = new ObservableCollection<GymClassComboBoxResponse>();
        SidebarView = sidebarView;
        Navigation = navigation;
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(ClientId), item=> true);
        _clientHttpClient = clientHttpClient;
        _classBookingHttpClient = classBookingHttpClient;
        _gymClassHttpClient = gymClassHttpClient;
        _scheduledClassHttpClient = scheduledClassHttpClient;
        _ = LoadGymClassesAsync();
        AddClassBookingCommand = new AsyncRelayCommand(item => AddClassBookingAsync(), item => true);
    }

    private async Task AddClassBookingAsync()
    {
        Result<ClassBookingInfoResponse> result = await _classBookingHttpClient.PostClassBookingAsync(ClassBookingRequest);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        Navigation.NavigateTo<ClientDetailsViewModel>(ClientId);
    }

    private async Task LoadScheduledClassesComboBox()
    {
        Result<ObservableCollection<ScheduledClassComboBoxResponse>> result = await _scheduledClassHttpClient.GetScheduledClassesComboBox(SelectedGymClass.GymClassId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        ScheduledClasses = result.Value!;
    }

    private async Task LoadGymClassesAsync()
    {
        Result<ObservableCollection<GymClassComboBoxResponse>> result = await _gymClassHttpClient.GetGymClassComboBoxResponses();
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        GymClasses = result.Value!;

    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            ClientId = clientId;
            ClassBookingRequest.ClientId = clientId;
            _ = LoadClientName(clientId);
        }
    }

    private async Task LoadClientName(Guid clientId)
    {
        Client = await _clientHttpClient.GetClientNameById(clientId);
    }
}
