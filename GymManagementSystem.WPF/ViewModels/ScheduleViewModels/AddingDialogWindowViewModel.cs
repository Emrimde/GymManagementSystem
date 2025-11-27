using GymManagementSystem.Core.DTO.TrainerTimeOff;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using System;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ScheduleViewModels;

public class AddingDialogWindowViewModel : ViewModel
{
    private readonly Guid _trainerId;
    private readonly TrainerHttpClient _trainerHttpClient;

    public string Subject { get; set; }
    public DateTime StartTime { get; set; } = DateTime.Now;
    public DateTime EndTime { get; set; } = DateTime.Now.AddHours(1);

    public event Action<bool>? CloseRequested;

    public AddingDialogWindowViewModel(Guid trainerId, TrainerHttpClient trainerHttpClient)
    {
        _trainerId = trainerId;
        _trainerHttpClient = trainerHttpClient;

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
