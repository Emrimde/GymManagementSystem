using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.WPF.Core;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduleViewModels;

public class EditingDialogWindowViewModel : ViewModel
{
    private readonly Guid _trainerId;

    public string Subject { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime EndTime { get; set; } = DateTime.Now.AddHours(1);

    public event Action<bool>? CloseRequested;

    public EditingDialogWindowViewModel(Guid trainerId)
    {
        _trainerId = trainerId;
       

        SaveCommand = new RelayCommand(_ => Save(), item => true);
    }

    private async void Save()
    {
        CloseRequested?.Invoke(true);
    }

    public ICommand SaveCommand { get; }

    public TrainerTimeOffAddRequest BuildDto()
    {
        return new TrainerTimeOffAddRequest
        {
            TrainerId = _trainerId,
            Start = DateTime.SpecifyKind(StartTime, DateTimeKind.Utc),
            End = DateTime.SpecifyKind(EndTime, DateTimeKind.Utc),
            Reason = Subject
        };
    }
}
