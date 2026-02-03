using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.PersonalBooking;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduleViewModels;

public class BookingDetailsViewModel : ViewModel
{
    private readonly PersonalBookingHttpClient _bookingHttp;

    public Guid BookingId { get; private set; }
    public bool ShouldDelete { get; private set; } = false;
    public bool ShouldSetToPaid { get; private set; } = false;
    public bool IsNotPaid { get; private set; } = true;

    public string ClientName { get; set; } = string.Empty;
    public BookingStatus Statuss { get; set; }
    public string Price { get; set; } = string.Empty;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand SetStatusToPaidCommand { get; }

    public event Action<bool>? CloseRequested;

    public BookingDetailsViewModel(PersonalBookingHttpClient http)
    {
        _bookingHttp = http;

        SaveCommand = new RelayCommand(item => Save(), _ => true);
        DeleteCommand = new RelayCommand(item => Delete(), _ => true);
        SetStatusToPaidCommand = new RelayCommand(_ => SetStatusToPaid(), _ => true);
    }

    private async Task _LoadPersonalBooking(Guid bookingId)
    {
        Result<PersonalBookingInfoResponse> result = await _bookingHttp.GetPersonalBookingAsync(bookingId);
        if (result.IsSuccess)
        {
            Statuss = result.Value!.Status;
            Price = result.Value!.Price;
            if(result.Value.Status == BookingStatus.PaidByClient)
            {
                IsNotPaid = false;
            }
            else
            {
                IsNotPaid = true;
            }
        }
    }

   
    //   Loading appointment
    public async void LoadFromAppointment(ScheduleAppointment appt)
    {
        BookingId = (Guid)appt.Id;

        ClientName = appt.Subject;
        Start = appt.StartTime;
        End = appt.EndTime;

        await _LoadPersonalBooking(BookingId); 
        
        OnPropertyChanged(nameof(ClientName));
        OnPropertyChanged(nameof(Start));
        OnPropertyChanged(nameof(End));
        OnPropertyChanged(nameof(Statuss));    
        OnPropertyChanged(nameof(IsNotPaid));    
        OnPropertyChanged(nameof(Price));    
    }

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

    private void SetStatusToPaid()
    {
        ShouldSetToPaid = true;

        CloseRequested?.Invoke(true);

    }

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
