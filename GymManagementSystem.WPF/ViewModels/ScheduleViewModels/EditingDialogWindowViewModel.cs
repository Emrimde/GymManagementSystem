using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.WPF.Core;
using Syncfusion.UI.Xaml.Scheduler;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduleViewModels;

public class EditingDialogWindowViewModel : ViewModel
{
    private readonly Guid _trainerId;

    public Guid TimeOffId { get; set; }
    public string Subject { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public bool ShouldDelete { get; private set; } = false;

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand CancelCommand { get; }

    public event Action<bool>? CloseRequested;

    public EditingDialogWindowViewModel(Guid trainerId)
    {
        _trainerId = trainerId;

        SaveCommand = new RelayCommand(_ => Save(), item => true);
        DeleteCommand = new RelayCommand(item => Delete(), item => true);
        CancelCommand = new RelayCommand(item => Cancel(), item => true);
    }

    // ============================================
    //       ZAŁADOWANIE DANYCH Z APPOINTMENT
    // ============================================

    public void LoadFromAppointment(ScheduleAppointment appt)
    {
        // TimeOffId przechowujemy w Appointment.Id
        TimeOffId = (Guid)appt.Id; // schedule appointment id na timeoffid?

        StartTime = appt.StartTime;
        EndTime = appt.EndTime;
        Subject = appt.Subject ?? "Time Off";

        OnPropertyChanged(nameof(StartTime));
        OnPropertyChanged(nameof(EndTime));
        OnPropertyChanged(nameof(Subject));
    }

    // ============================================
    //       AKCJE — ZAPIS / USUNIĘCIE / ANULUJ
    // ============================================

    private async void Save()
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

    // ============================================
    //    DTO do UPDATE (NOWE START, END, REASON)
    // ============================================

    public TrainerTimeOffUpdateRequest BuildDto()
    {
        return new TrainerTimeOffUpdateRequest
        {
            Id = TimeOffId,
            TrainerId = _trainerId,
            Start = DateTime.SpecifyKind(StartTime, DateTimeKind.Utc),
            End = DateTime.SpecifyKind(EndTime, DateTimeKind.Utc),
            Reason = Subject
        };
    }
}
