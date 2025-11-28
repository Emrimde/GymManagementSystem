using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduleViewModels;

public class BookingDetailsViewModel : ViewModel
{
    private readonly PersonalBookingHttpClient _bookingHttp;

    public Guid BookingId { get; private set; }
    public bool ShouldDelete { get; private set; } = false;

    public string ClientName { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand CancelCommand { get; }

    public event Action<bool>? CloseRequested;

    public BookingDetailsViewModel(PersonalBookingHttpClient http)
    {
        _bookingHttp = http;

        SaveCommand = new RelayCommand(_ => Save(), _ => true);
        DeleteCommand = new RelayCommand(_ => Delete(), _ => true);
        CancelCommand = new RelayCommand(_ => Cancel(), _ => true);
    }

    // ---------------------------
    //   ZAŁADOWANIE Z APPOINTMENT
    // ---------------------------
    public void LoadFromAppointment(ScheduleAppointment appt)
    {
        // BookingId MUSI być ustawiony w appointment.Id w kalendarzu!
        BookingId = (Guid)appt.Id;

        ClientName = appt.Subject;
        Start = appt.StartTime;
        End = appt.EndTime;

        OnPropertyChanged(nameof(ClientName));
        OnPropertyChanged(nameof(Start));
        OnPropertyChanged(nameof(End));
    }

    // ---------------------------
    //   AKCJE
    // ---------------------------

    private void Save()
    {
        ShouldDelete = false;
        CloseRequested?.Invoke(true);
    }

    private void Delete()
    {
        ShouldDelete = true;
        CloseRequested?.Invoke(true);
    }

    private void Cancel()
    {
        CloseRequested?.Invoke(false);
    }

    // ---------------------------
    //  DTO DO UPDATE
    // ---------------------------

    public PersonalBookingUpdateRequest BuildDto()
    {
        return new PersonalBookingUpdateRequest
        {
            Id = BookingId,
            Start = Start.ToUniversalTime(),
            End = End.ToUniversalTime()
        };
    }
}
