using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ScheduleViewModels;
using GymManagementSystem.WPF.Views.ScheduleWindows;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GymManagementSystem.WPF.ViewModels.Trainer;
public class TrainerScheduleViewModel : ViewModel, IParameterReceiver
{
    public ScheduleAppointmentCollection Events { get; set; } = new();
    public ICommand OpenEditorCommand { get; }
    public SidebarViewModel SidebarView { get; set; }
    private readonly PersonalBookingHttpClient _bookingHttpClient;
    private readonly ClientHttpClient _clientHttpClient;
    private readonly TrainerHttpClient _trainerHttpClient;

    private Guid _trainerId;

    public TrainerScheduleViewModel(
        TrainerHttpClient trainerHttpClient,
        ClientHttpClient clientHttpClient,
        SidebarViewModel sidebarView,
        PersonalBookingHttpClient bookingHttpClient)
    {
        SidebarView = sidebarView;
        _trainerHttpClient = trainerHttpClient;
        _bookingHttpClient = bookingHttpClient;
        _clientHttpClient = clientHttpClient;
        OpenEditorCommand = new RelayCommand(item => OpenEditor(item!), item => true);
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _trainerId = id;
            _ = LoadAppointmentsAsync();
        }
    }

    
    //choice dialog
    private async void OpenEditor(object obj)
    {
        if (obj is not AppointmentEditorOpeningEventArgs e)
            return;

        e.Cancel = true; // blokujemy default edytor Syncfusion

        // 1. Kliknieto pusty slot - dodaj 
        if (e.Appointment == null)
        {
            await HandleAddAction(e);
            return;
        }

        // 2. Kliknieto istniejacy event - edytuj
        await HandleEditAction(e);
    }

    
    //Adding
    private async Task HandleAddAction(AppointmentEditorOpeningEventArgs e)
    {
        var choiceDialog = new AddChoiceDialog();
        if (choiceDialog.ShowDialog() != true)
            return;

        if (choiceDialog.Choice == "TimeOff")
        {
            await AddTimeOffAsync(e);
        }
    }

    private DateTime ResolveStartFromEvent(AppointmentEditorOpeningEventArgs e)
    {
        if (e.DateTime < DateTime.Now)
        {
            if (DateTime.Now.Minute % 15 != 0)
            {
                DateTime now = DateTime.Now;
                int minutesToAdd = (15 - (now.Minute % 15)) % 15; 

                DateTime rounded = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0)
                                   .AddMinutes(minutesToAdd);
                return rounded;
            }
        }
        return e.DateTime;
    }

    private async Task AddTimeOffAsync(AppointmentEditorOpeningEventArgs e)
    {
        DateTime start = ResolveStartFromEvent(e);
        DateTime end = start.AddHours(1);

        AddingDialogWindowViewModel vm = new AddingDialogWindowViewModel(_trainerId,_trainerHttpClient,start,end);
        AddingDialogWindow dialog = new AddingDialogWindow { DataContext = vm };
        if (dialog.ShowDialog() == true)
        {
            TrainerTimeOffAddRequest? dto = vm.BuildDto();
            if(dto == null)
            {
                MessageBox.Show("Invalid input. Please check your data and try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Result<Unit> result = await _trainerHttpClient.PostTrainerTimeOff(dto);
            if (!result.IsSuccess)
            {
                MessageBox.Show($"{result.GetUserMessage()}","Error",MessageBoxButton.OK, MessageBoxImage.Error);
            }

            await LoadAppointmentsAsync();
        }
    }

   
    //  Edit
    private async Task HandleEditAction(AppointmentEditorOpeningEventArgs e)
    {
        string type = GetTypeFromAppointment(e.Appointment);

        if (type == "TimeOff")
        {
            await EditTimeOffAsync(e);
        }
        else if (type == "Booking")
        {
            await EditBookingAsync(e);
        }
    }

    private async Task EditTimeOffAsync(AppointmentEditorOpeningEventArgs e)
    {
        EditingDialogWindowViewModel vm = new EditingDialogWindowViewModel(_trainerId, _trainerHttpClient);
        EditingDialogWindow dialog = new EditingDialogWindow { DataContext = vm };
        vm.LoadFromAppointment(e.Appointment);


        if (dialog.ShowDialog() == true)
        {
            Result<TrainerTimeOff> updateResult = null!;

            if (vm.ShouldDelete)
            {
                Result<Unit> deleteResult = await _trainerHttpClient.DeleteTrainerTimeOffAsync(vm.TimeOffId);

                if (!deleteResult.IsSuccess)
                {
                    MessageBox.Show(deleteResult.GetUserMessage());
                    return;
                }
            }
           
            else
            {
                updateResult = await _trainerHttpClient.UpdateAsync(vm.TimeOffId, vm.BuildDto());

                if (!updateResult.IsSuccess)
                {
                    MessageBox.Show(updateResult.GetUserMessage());
                    return;
                }
            }

            await LoadAppointmentsAsync();
        }
    }


    private async Task EditBookingAsync(AppointmentEditorOpeningEventArgs e)
    {
        BookingDetailsViewModel vm = new BookingDetailsViewModel(_bookingHttpClient);
        BookingDetailsDialog dialog = new BookingDetailsDialog { DataContext = vm };

        vm.LoadFromAppointment(e.Appointment);

        if (dialog.ShowDialog() == true)
        {
            if (vm.ShouldDelete)
            {
                await _bookingHttpClient.DeleteAsync(vm.BookingId);
            }
            else if (vm.ShouldSetToPaid)
            {
                Result<PersonalBookingInfoResponse> result = await _bookingHttpClient.SetStatusToPaidAsync(vm.BookingId);
                if (!result.IsSuccess)
                {
                    MessageBox.Show($"{result.GetUserMessage()}");
                }
            }
            await LoadAppointmentsAsync();
        }
    }
    private readonly Random _rng = new();

    private Color GeneratePastel()
    {
        byte r = (byte)_rng.Next(128, 256);
        byte g = (byte)_rng.Next(128, 256);
        byte b = (byte)_rng.Next(128, 256);

        return Color.FromArgb(255, r, g, b); 
    }
    private string GetTypeFromAppointment(ScheduleAppointment appt)
    {
        return appt.Notes ?? "Available";
    }


    //loading all blocks
    private async Task LoadAppointmentsAsync()
    {
        var schedule = await _trainerHttpClient.GetSchedule(_trainerId);

        var appointments = new ScheduleAppointmentCollection();

        foreach (var day in schedule.Value!.Days)
        {
            
            foreach (var item in day.Items)
            {
                if(item.Type == TrainerScheduleItemType.Available)
                {
                    continue;
                }
                var appt = new ScheduleAppointment
                {
                    StartTime = item.Start.ToLocalTime(),
                    EndTime = item.End.ToLocalTime(),
                    Subject = item.Type switch
                    {
                        TrainerScheduleItemType.Available => "Available",
                        TrainerScheduleItemType.TimeOff => "Time Off",
                        TrainerScheduleItemType.Booked => item.ClientName ?? "Booked",
                        _ => "Available"
                    },
                    Id = item.Type switch
                    {
                        TrainerScheduleItemType.TimeOff => item.TimeOffId,
                        TrainerScheduleItemType.Booked => item.BookingId,
                        _ => Guid.NewGuid()
                    },

                    Notes = item.Type switch
                    {
                        TrainerScheduleItemType.TimeOff => "TimeOff",
                        TrainerScheduleItemType.Booked => "Booking",
                        _ => "Available"
                    }
                };

                if (item.Type == TrainerScheduleItemType.TimeOff)
                {
                    appt.AppointmentBackground =
                        new SolidColorBrush(System.Windows.Media.Color.FromArgb(204, 0, 180, 0)); 
                }
                else if (item.Type == TrainerScheduleItemType.Booked)
                {
                    appt.AppointmentBackground = new SolidColorBrush(GeneratePastel());
                }

                appointments.Add(appt);
            }
        }

        Events = appointments;
        OnPropertyChanged(nameof(Events));
    }
}
