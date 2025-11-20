using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GymManagementSystem.WPF.ViewModels.Termination;

public class TerminationViewModel : ViewModel
{
    private readonly TerminationHttpClient _httpClient;
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigationService;
    public INavigationService Navigation
    {
        get { return _navigationService; }
        set { _navigationService = value; OnPropertyChanged(); }
    }

    private ObservableCollection<TerminationResponse> _terminations;
    public ObservableCollection<TerminationResponse> Terminations
    {
        get { return _terminations; }
        set
        {
            _terminations = value;
            OnPropertyChanged();
        }
    }


    public TerminationViewModel(SidebarViewModel sidebarView, INavigationService navigation, TerminationHttpClient httpClient)
    {
        SidebarView = sidebarView;
        Navigation = navigation;
        Terminations = new ObservableCollection<TerminationResponse>();
        _httpClient = httpClient;   
        _ = LoadTerminations();
    }

    private async Task LoadTerminations()
    {
        ObservableCollection<TerminationResponse> terminations = await _httpClient.GetTerminationsAsync();
        Terminations = terminations;
    }

    

    
    
}
