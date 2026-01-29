using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using Syncfusion.UI.Xaml.Scheduler;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduleViewModels;

public class EditingDialogWindowViewModel : ViewModel
{
    private readonly Guid _trainerId;
    private readonly TrainerHttpClient _trainerHttpClient;
    public Guid TimeOffId { get; set; }

    private string _reason = string.Empty;
    public DateTime? SelectedDate { get; set; }

    public ObservableCollection<string> TimeSlots { get; }
    public string SelectedStartSlot { get; set; } = "";
    public string SelectedEndSlot { get; set; } = "";


    public string Reason
    {
        get { return _reason; }
        set { _reason = value; OnPropertyChanged(); }
    }

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool ShouldDelete { get; private set; } = false;
    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand LoadReasonCommand { get; }

    public event Action<bool>? CloseRequested;

    public EditingDialogWindowViewModel(Guid trainerId, TrainerHttpClient trainerHttpClient)
    {
        _trainerId = trainerId;
        _trainerHttpClient = trainerHttpClient;
        SaveCommand = new RelayCommand(item => Save(), item => true);
        DeleteCommand = new AsyncRelayCommand(item => DeleteAsync(), item => true);
        CancelCommand = new RelayCommand(item => Cancel(), item => true);
        LoadReasonCommand = new AsyncRelayCommand(item => LoadReasonDataAsync(), item => true);
        TimeSlots = GenerateTimeSlots();
    }

    private async Task LoadReasonDataAsync()
    {
        Result<TrainerTimeOffReasonResponse> result =
            await _trainerHttpClient.GetReasonTimeOffAsync(TimeOffId);

        if (result.IsSuccess)
            Reason = result.Value!.Reason ?? "";
    }


    //Loading data from clicked appointment
    public void LoadFromAppointment(ScheduleAppointment appt)
    {
        TimeOffId = (Guid)appt.Id;

        StartTime = appt.StartTime;
        EndTime = appt.EndTime;

        SelectedDate = StartTime.Date;
        SelectedStartSlot = RoundTo15(StartTime.TimeOfDay).ToString(@"hh\:mm");
        SelectedEndSlot = RoundTo15(EndTime.TimeOfDay).ToString(@"hh\:mm");

        OnPropertyChanged(nameof(SelectedDate));
        OnPropertyChanged(nameof(SelectedStartSlot));
        OnPropertyChanged(nameof(SelectedEndSlot));

        LoadReasonCommand.Execute(this);
    }
    private TimeSpan RoundTo15(TimeSpan t)
    {
        int minutes = (int)(Math.Round(t.TotalMinutes / 15.0) * 15);
        return TimeSpan.FromMinutes(minutes);
    }

    private ObservableCollection<string> GenerateTimeSlots()
    {
        var list = new ObservableCollection<string>();
        TimeSpan start = new TimeSpan(7, 0, 0);
        TimeSpan end = new TimeSpan(22, 0, 0);

        for (var t = start; t <= end; t += TimeSpan.FromMinutes(15))
            list.Add(t.ToString(@"hh\:mm"));

        return list;
    }


    //actions
    private void Save()
    {
        ShouldDelete = false;
        CloseRequested?.Invoke(true);
    }

    private async Task DeleteAsync()
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
        var start = SelectedDate!.Value.Date + TimeSpan.Parse(SelectedStartSlot);
        var end = SelectedDate.Value.Date + TimeSpan.Parse(SelectedEndSlot);

        return new TrainerTimeOffUpdateRequest
        {
            Id = TimeOffId,
            TrainerId = _trainerId,
            Start = start.ToUniversalTime(),
            End = end.ToUniversalTime(),
            Reason = Reason
        };
    }

}
