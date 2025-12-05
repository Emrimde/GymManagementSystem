using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.TrainerRate;

public class TrainerRateViewModel : ViewModel, IParameterReceiver
{
    private INavigationService _navigation;
    public Guid TrainerContractId { get; set; }
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    public ICommand OpenAddTrainerRateView { get;  }
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
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _ = LoadTrainerRates(id);
            TrainerContractId = id;
        }
    }

    private async Task LoadTrainerRates(Guid id)
    {
        Result<ObservableCollection<TrainerRateResponse>> result = await _trainerHttpClient.GetTrainerRatesAsync(id);
        if (result.IsSuccess)
        {
            foreach (var item in result.Value)
            {
                TrainerRates.Add(item);
                
            }
        }
    }
}
