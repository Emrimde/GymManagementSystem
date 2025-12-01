using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using System.Collections.ObjectModel;
using System.Windows.Input;

public class AddingDialogWindowViewModel : ViewModel
{
    private readonly Guid _trainerId;
    private readonly TrainerHttpClient _trainerHttpClient;

    public string Subject { get; set; }

    public DateTime? SelectedDate { get; set; }
    public ObservableCollection<string> TimeSlots { get; set; }
    public string SelectedStartSlot { get; set; }
    public string SelectedEndSlot { get; set; }

    public event Action<bool>? CloseRequested;
    public ICommand SaveCommand { get; }

    public AddingDialogWindowViewModel(
        Guid trainerId,
        TrainerHttpClient trainerHttpClient,
        DateTime start,
        DateTime end)
    {
        _trainerId = trainerId;
        _trainerHttpClient = trainerHttpClient;

        TimeSlots = GenerateTimeSlots();

        // ustawiamy datę
        SelectedDate = start.Date;

        // ustawiamy sloty 15-minutowe
        SelectedStartSlot = RoundTo15(start.TimeOfDay).ToString(@"hh\:mm");
        SelectedEndSlot = RoundTo15(end.TimeOfDay).ToString(@"hh\:mm");

        SaveCommand = new RelayCommand(_ => Save(), _ => true);
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

    private void Save()
    {
        CloseRequested?.Invoke(true);
    }

    public TrainerTimeOffAddRequest BuildDto()
    {
        var start = SelectedDate.Value.Date + TimeSpan.Parse(SelectedStartSlot);
        var end = SelectedDate.Value.Date + TimeSpan.Parse(SelectedEndSlot);

        return new TrainerTimeOffAddRequest
        {
            TrainerId = _trainerId,
            Start = DateTime.SpecifyKind(start, DateTimeKind.Utc),
            End = DateTime.SpecifyKind(end, DateTimeKind.Utc),
            Reason = Subject
        };
    }
}
