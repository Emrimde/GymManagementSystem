using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using OxyPlot;
using OxyPlot.Series;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels;
public class DashboardViewModel : ViewModel
{
    public PlotModel VisitsModel { get; }
    private readonly DashboardHttpClient _dashboardHttpClient;

    private DashboardKpiResponse _dashboardKpi ;

    public DashboardKpiResponse DashboardKPI
    {
        get { return _dashboardKpi; }
        set { _dashboardKpi = value; OnPropertyChanged(); }
    }

    public SidebarViewModel SidebarView { get; }
    public ICommand OpenRegisterViewCommand { get; }
    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }

    public DashboardViewModel(INavigationService navigationService, SidebarViewModel sidebarViewModel, DashboardHttpClient dashboardHttpClient)
    {
        _dashboardHttpClient = dashboardHttpClient;
        _ = LoadActiveClientsCountAsync();
        _navigation = navigationService;
        OpenRegisterViewCommand = new RelayCommand(o => Navigation.NavigateTo<RegisterViewModel>(), o => true);
        SidebarView = sidebarViewModel;


        VisitsModel = new PlotModel
        {
            Title = "Wizyty",
            Background = OxyColors.Transparent,

            TextColor = OxyColors.White,
            TitleColor = OxyColors.White,
            PlotAreaBorderColor = OxyColors.Gray
        };

        var series = new LineSeries();
        series.Points.Add(new DataPoint(0, 12));
        series.Points.Add(new DataPoint(1, 18));
        series.Points.Add(new DataPoint(2, 9));

        VisitsModel.Series.Add(series);
    }

    private async Task LoadActiveClientsCountAsync()
    {
      Result<DashboardKpiResponse> result = await _dashboardHttpClient.GetAllDashboardKpiAsync();
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
            return;
        }
        DashboardKPI = result.Value!; 
    }
}
