using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ScheduleViewModels;
using GymManagementSystem.WPF.Views.ScheduleWindows;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Input;
using System.Windows.Media;

namespace GymManagementSystem.WPF.ViewModels.Trainer;

public class TrainerScheduleViewModel : ViewModel, IParameterReceiver
{
    public ScheduleAppointmentCollection Events { get; set; } = new();

    public ICommand OpenEditorCommand { get; }

    private string GetTypeFromAppointment(ScheduleAppointment appt)
{
    return appt.Subject switch
    {
        "Time Off" => "TimeOff",
        "Booked"   => "Booking",
        _          => "Available"
    };
}

    private async void OpenEditor(object? item)
    {
        if (item is AppointmentEditorOpeningEventArgs e)
        {
            e.Cancel = true;

            if (e.Appointment == null)
            {
                var dialogVm = new AddingDialogWindowViewModel(_trainerId, _httpClient);
                var dialog = new AddingDialogWindow { DataContext = dialogVm };

                if (dialog.ShowDialog() == true)
                {
                    // pobieramy dane z dialogu
                    var dto = dialogVm.BuildDto();

                    // wysyłamy do API
                    await _trainerHttpClient.PostTrainerTimeOff(dto);

                    // odświeżamy grafik
                    await LoadAppointmentsAsync();
                }
            }
            else
            {

                EditingDialogWindowViewModel viewmodel = new EditingDialogWindowViewModel(_trainerId);
                EditingDialogWindow dialog = new EditingDialogWindow { DataContext = viewmodel };
                if (dialog.ShowDialog() == true)
                {
                    // pobieramy dane z dialogu
                    var dto = viewmodel.BuildDto();

                    // wysyłamy do API
                    await _trainerHttpClient.PostTrainerTimeOff(dto);

                    // odświeżamy grafik
                    await LoadAppointmentsAsync();
                }
            }
        }
    }

    private async Task LoadAppointmentsAsync()
    {
        TrainerScheduleResponse schedule = await _trainerHttpClient.GetSchedule(_trainerId);

        var appointments = new ScheduleAppointmentCollection();

        foreach (var day in schedule.Days)
        {
            foreach (var item in day.Items)
            {
                var appointment = new ScheduleAppointment
                {
                    StartTime = item.Start,
                    EndTime = item.End,
                    IsAllDay = false
                };

                switch (item.Type)
                {
                    case TrainerScheduleItemType.Available:
                        appointment.Subject = "Available";
                        appointment.AppointmentBackground =
                            new SolidColorBrush(Color.FromArgb(80, 0x33, 0x99, 0x33));
                        break;

                    case TrainerScheduleItemType.Booked:
                        appointment.Subject = item.ClientName ?? "Booked";
                        appointment.AppointmentBackground =
                            new SolidColorBrush(Color.FromArgb(80, 0x1B, 0xA1, 0xE2));
                        break;

                    case TrainerScheduleItemType.TimeOff:
                        appointment.Subject = "Time Off";
                        appointment.AppointmentBackground =
                            new SolidColorBrush(Color.FromArgb(80,0xD8, 0x00, 0x73));
                        appointment.IsAllDay = false;
                        break;
                }

                appointments.Add(appointment);
            }
        }

        Events = appointments;
        OnPropertyChanged(nameof(Events));
    }

    private Guid _trainerId;
    private readonly TrainerHttpClient _trainerHttpClient;
    private readonly TrainerHttpClient _httpClient;
    public SidebarViewModel SidebarView { get; set; }

    public TrainerScheduleViewModel(
        TrainerHttpClient httpClient,
        SidebarViewModel sidebarView,
        INavigationService navigation)
    {
        _httpClient = httpClient;
        _trainerHttpClient = httpClient;
        SidebarView = sidebarView;
        OpenEditorCommand = new RelayCommand(item => OpenEditor(item), item => true);
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _trainerId = id;
            _ = LoadAppointmentsAsync(); // 💥 auto-ładowanie grafiku trenera
        }
    }
}
