using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using System.Collections.ObjectModel;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.PersonalBooking;
public class PersonalBookingViewModel : ViewModel, IParameterReceiver
{
    public Guid ClientId { get; set; }
    private readonly PersonalBookingHttpClient _personalBookingHttpClient;
    public ICommand LoadPersonalBookingCommand { get; }
    public ICommand OpenEditPersonalBookingCommand { get; }
    public ICommand OpenPersonalTrainingAddViewCommand { get; }
    public ICommand DeletePersonalBookingCommand { get; }
    public ICommand CancelCommand { get; }
    private ObservableCollection<PersonalBookingResponse> _personalBookings = new();

    public ObservableCollection<PersonalBookingResponse> PersonalBookings
    {
        get { return _personalBookings; }
        set
        {
            if (_personalBookings != value)
            {
                _personalBookings = value;
                OnPropertyChanged();
            }
        }
    }
    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation { get; }

    public PersonalBookingViewModel(INavigationService navigation, SidebarViewModel sidebarView, PersonalBookingHttpClient personalBookingHttpClient)
    {
        _personalBookingHttpClient = personalBookingHttpClient;
        Navigation = navigation;
        SidebarView = sidebarView;
        LoadPersonalBookingCommand = new AsyncRelayCommand(item => LoadPersonalBookingAsync(), item => true);
        OpenEditPersonalBookingCommand = new RelayCommand(item => Navigation.NavigateTo<PersonalBookingUpdateViewModel>(item!), item => true);
        DeletePersonalBookingCommand = new AsyncRelayCommand(item => DeletePersonalBookingAsync(item), item => true);
        OpenPersonalTrainingAddViewCommand = new RelayCommand(item => Navigation.NavigateTo<PersonalBookingAddViewModel>(ClientId), item => true);
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(ClientId), item => true);

    }

    private async Task DeletePersonalBookingAsync(object item)
    {
        if (item is Guid personalBookingId)
        {
            MessageBoxResult mbResult = MessageBox.Show("Are you sure to delete this personal booking?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mbResult == MessageBoxResult.Yes)
            {
                Result<Unit> result = await _personalBookingHttpClient.DeletePersonalBookingAsync(personalBookingId);
                if (!result.IsSuccess)
                {
                    MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                LoadPersonalBookingCommand.Execute(null);
            }

        }
    }

    private async Task LoadPersonalBookingAsync()
    {
        Result<ObservableCollection<PersonalBookingResponse>> result = await _personalBookingHttpClient.GetPersonalBookingsAsync(ClientId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        PersonalBookings = result.Value!;
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            ClientId = clientId;
        }
    }
}
