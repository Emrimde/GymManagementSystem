using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Staff.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.TrainerRate;

public class TrainerRateViewModel : ViewModel, IParameterReceiver
{
    public Guid TrainerContractId { get; set; }
 
    public INavigationService Navigation { get; set; }

    public ObservableCollection<StatusFilter> StatusFilters { get; } = new()
{
    new StatusFilter { Label = "All", Value = null },
    new StatusFilter { Label = "Active", Value = true }
};

    private bool? _selectedIsActive = null;

    public bool? SelectedIsActive
    {
        get => _selectedIsActive;
        set
        {
            _selectedIsActive = value;
            OnPropertyChanged();
            LoadTrainerRatesCommand.Execute(null);
        }
    }


    public ICommand OpenAddTrainerRateView { get;  }
    public ICommand LoadTrainerRatesCommand { get; }
    public ObservableCollection<TrainerRateResponse> TrainerRates { get; set; }

    private readonly TrainerHttpClient _trainerHttpClient;
    public SidebarViewModel SidebarView { get; set; }
    public TrainerRateViewModel(TrainerHttpClient trainerHttpClient, INavigationService navigation, SidebarViewModel sidebarView)
    {
        Navigation = navigation;
        _trainerHttpClient = trainerHttpClient;
        TrainerRates = new ObservableCollection<TrainerRateResponse>();
        SidebarView = sidebarView;
        OpenAddTrainerRateView = new RelayCommand(item => Navigation.NavigateTo<TrainerRateAddViewModel>(item), item => true);
        LoadTrainerRatesCommand = new AsyncRelayCommand(item => LoadTrainerRatesAsync(), item => true);

    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            TrainerContractId = id;
            _ = LoadTrainerRatesAsync();
        }
    }

    private async Task LoadTrainerRatesAsync()
    {
        TrainerRates.Clear();

        var result = await _trainerHttpClient.GetTrainerRatesAsync(TrainerContractId, SelectedIsActive);

        if (!result.IsSuccess) return;

        var items = result.Value;

        foreach (var item in items)
            TrainerRates.Add(item);
    }

}
