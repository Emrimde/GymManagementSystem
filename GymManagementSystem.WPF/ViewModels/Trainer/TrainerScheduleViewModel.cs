using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ScheduleViewModels;
using GymManagementSystem.WPF.Views.ScheduleWindows;
using Syncfusion.UI.Xaml.Scheduler;
using Syncfusion.Windows.Controls;
using System;

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
        OpenEditorCommand = new RelayCommand(item => OpenEditor(item), item => true);
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _trainerId = id;
            _ = LoadAppointmentsAsync();
        }
    }

    // ---------------------------------
    //          CORE LOGIC
    // ---------------------------------

    private async void OpenEditor(object obj)
    {
        if (obj is not AppointmentEditorOpeningEventArgs e)
            return;

        e.Cancel = true; // blokujemy default edytor Syncfusion

        // 1. Kliknięto pusty slot → dodaj cos
        if (e.Appointment == null)
        {
            await HandleAddAction(e);
            return;
        }

        // 2. Kliknięto istniejący event → edytuj coś
        await HandleEditAction(e);
    }

    // ---------------------------------------
    //            DODAWANIE
    // ---------------------------------------

    private async Task HandleAddAction(AppointmentEditorOpeningEventArgs e)
    {
        var choiceDialog = new AddChoiceDialog();
        if (choiceDialog.ShowDialog() != true)
            return;

        if (choiceDialog.Choice == "TimeOff")
        {
            await AddTimeOffAsync(e);
        }
        else if (choiceDialog.Choice == "Booking")
        {
            await AddBookingAsync(e);
        }
    }

    private DateTime ResolveStartFromEvent(AppointmentEditorOpeningEventArgs e)
    {
        string[] props = { "SelectedDate", "Start", "StartTime", "SelectedDateTime" };

        foreach (string name in props)
        {
            System.Reflection.PropertyInfo propInfo = e.GetType().GetProperty(name);
            if (propInfo == null)
                continue;

            object val = propInfo.GetValue(e);
            if (val == null)
                continue;

            // jeśli to DateTime
            if (val is DateTime dt)
                return dt;

            // jeśli to Nullable<DateTime> (boxed)
            if (val.GetType() == typeof(DateTime?))
            {
                var boxed = (DateTime?)val;
                if (boxed.HasValue)
                    return boxed.Value;
                continue;
            }

            // jeśli to DateTimeOffset
            if (val is DateTimeOffset dto)
                return dto.DateTime;

            // jeśli to string (np. "2025-11-27T11:06:04Z"), spróbuj sparsować
            if (val is string s)
            {
                if (DateTime.TryParse(s, out DateTime parsed))
                    return parsed;
            }
        }

        // fallback — lokalny czas
        return DateTime.Now;
    }



    private async Task AddTimeOffAsync(AppointmentEditorOpeningEventArgs e)
    {
        var start = ResolveStartFromEvent(e);
        var end = start.AddHours(1);

        var vm = new AddingDialogWindowViewModel(
    _trainerId,
    _trainerHttpClient,
    start,
    end);
        var dialog = new AddingDialogWindow { DataContext = vm };
        if (dialog.ShowDialog() == true)
        {
            var dto = vm.BuildDto();
            await _trainerHttpClient.PostTrainerTimeOff(dto); // result-pattern method
            await LoadAppointmentsAsync();
        }
    }

    private async Task AddBookingAsync(AppointmentEditorOpeningEventArgs e)
    {
        var start = ResolveStartFromEvent(e);
        var end = start.AddHours(1);

        var vm = new AddBookingDialogViewModel(_trainerId, start, end, _bookingHttpClient, _clientHttpClient, _trainerHttpClient);
        var dialog = new AddBookingDialog { DataContext = vm };
        if (dialog.ShowDialog() == true)
        {
            var dto = vm.BuildDto();
            await _bookingHttpClient.CreateAsync(dto); // result-pattern method
            await LoadAppointmentsAsync();
        }
    }


    // ---------------------------------------
    //               EDYCJA
    // ---------------------------------------

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
        var vm = new EditingDialogWindowViewModel(_trainerId);
        var dialog = new EditingDialogWindow { DataContext = vm };
        vm.LoadFromAppointment(e.Appointment);


        if (dialog.ShowDialog() == true)
        {
            Result<bool> deleteResult = null!;
            Result<TrainerTimeOff> updateResult = null!;

            if (vm.ShouldDelete)
            {
                deleteResult = await _trainerHttpClient.DeleteAsync(vm.TimeOffId);

                if (!deleteResult.IsSuccess)
                {
                    MessageBox.Show(deleteResult.ErrorMessage);
                    return;
                }
            }
           
            else
            {
                updateResult = await _trainerHttpClient.UpdateAsync(vm.TimeOffId, vm.BuildDto());

                if (!updateResult.IsSuccess)
                {
                    MessageBox.Show(updateResult.ErrorMessage);
                    return;
                }
            }

            await LoadAppointmentsAsync();
        }
    }


    private async Task EditBookingAsync(AppointmentEditorOpeningEventArgs e)
    {
        var vm = new BookingDetailsViewModel(_bookingHttpClient);
        var dialog = new BookingDetailsDialog { DataContext = vm };

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
                    MessageBox.Show($"{result.ErrorMessage}");
                }
            }
            //else
            //{
            //    await _bookingHttpClient.UpdateAsync(vm.BookingId, vm.BuildDto());
            //}

            await LoadAppointmentsAsync();
        }
    }
    private readonly Random _rng = new();

    private Color GeneratePastel()
    {
        byte r = (byte)_rng.Next(128, 256);
        byte g = (byte)_rng.Next(128, 256);
        byte b = (byte)_rng.Next(128, 256);

        return Color.FromArgb(255, r, g, b); // 80% opacity (204)
    }
    // ---------------------------------------
    private string GetTypeFromAppointment(ScheduleAppointment appt)
    {
        return appt.Notes ?? "Available";
    }


    // ładujemy wszystkie bloki
    private async Task LoadAppointmentsAsync()
    {
        var schedule = await _trainerHttpClient.GetSchedule(_trainerId);

        var appointments = new ScheduleAppointmentCollection();

        foreach (var day in schedule.Days)
        {
            
            foreach (var item in day.Items)
            {
                if(item.Type == TrainerScheduleItemType.Available)
                {
                    continue;
                }
                var appt = new ScheduleAppointment
                {
                    StartTime = item.Start,
                    EndTime = item.End,
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

                    // 🔥 DODAJ TO — NIE RUSZAJ KOLORÓW
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
                        new SolidColorBrush(System.Windows.Media.Color.FromArgb(204, 0, 180, 0)); // zielony 80%
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
