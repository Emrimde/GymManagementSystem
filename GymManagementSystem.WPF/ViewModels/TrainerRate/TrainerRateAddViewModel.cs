using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.TrainerRate;
public class TrainerRateAddViewModel : ViewModel, IParameterReceiver
{
    private INavigationService _navigation;
    public bool AddExisting { get; set; }
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    //public ICommand  { get; }
    public TrainerRateAddRequest TrainerRate { get; set; }

    private readonly TrainerHttpClient _trainerHttpClient;

    public TrainerRateAddViewModel(INavigationService navigation, TrainerHttpClient trainerHttpClient, SidebarViewModel sidebarView)
    {
        Navigation = navigation;
        _trainerHttpClient = trainerHttpClient;
        SidebarView = sidebarView;
        TrainerRate = new TrainerRateAddRequest();
        TimeSlots = GenerateTimeSlots();
        AddTrainerRateCommand = new AsyncRelayCommand(item => AddTrainerRateAsync(), item => true);
    }

    private async Task AddTrainerRateAsync()
    {
        if (!string.IsNullOrWhiteSpace(SelectedTimeSlot))
        {
            TimeSpan ts = TimeSpan.Parse(SelectedTimeSlot);
            TrainerRate.DurationInMinutes = (int)ts.TotalMinutes;
        }

        Result<TrainerRateInfoResponse> result = await _trainerHttpClient.AddTrainerRateAsync(TrainerRate);
        if (result.IsSuccess)
        {
            Navigation.NavigateTo<TrainerRateViewModel>();
        }
        // potem np. powrót
    }

    public ICommand AddTrainerRateCommand { get; }
    public SidebarViewModel SidebarView { get; set; }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            TrainerRate.TrainerContractId = id;
        }
    }


    private string? _selectedTimeSlot;
    public string? SelectedTimeSlot
    {
        get => _selectedTimeSlot;
        set { _selectedTimeSlot = value; OnPropertyChanged(); }
    }

    public ObservableCollection<string> TimeSlots { get; set; }
    private ObservableCollection<string> GenerateTimeSlots()
    {
        var list = new ObservableCollection<string>();
        TimeSpan from = new TimeSpan(1, 0, 0);
        TimeSpan to = new TimeSpan(2, 0, 0);
        var step = TimeSpan.FromMinutes(15);

        for (var t = from; t <= to; t += step)
        {
            list.Add(t.ToString(@"hh\:mm"));
        }

        return list;
    }
}
