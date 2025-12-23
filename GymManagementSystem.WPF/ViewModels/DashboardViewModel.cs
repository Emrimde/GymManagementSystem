using GymManagementSystem.Core.DTO.Dashboard;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace GymManagementSystem.WPF.ViewModels;
public class DashboardViewModel : ViewModel
{
    private PlotModel _visitsModel;

    public PlotModel VisitsModel
    {
        get { return _visitsModel; }
        set { _visitsModel = value; OnPropertyChanged(); }
    }

    private readonly DashboardHttpClient _dashboardHttpClient;

    private DashboardKpiResponse _dashboardKpi;

    public DashboardKpiResponse DashboardKPI
    {
        get { return _dashboardKpi; }
        set { _dashboardKpi = value; OnPropertyChanged(); }
    }
    private List<PointResponse> _points;

    public List<PointResponse> Points
    {
        get { return _points; }
        set { _points = value; OnPropertyChanged(); }
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
        _ = LoadPointsAsync();
    }

    private async Task LoadPointsAsync()
    {
        Result<List<PointResponse>> result = await _dashboardHttpClient.GetAllPointsAsync();
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        Points = result.Value!;
        CreateChart();
    }

    private void CreateChart()
    {
        VisitsModel = new PlotModel
        {
            Title = "Visits over week",
            Background = OxyColors.Transparent,
            TextColor = OxyColors.White,
            TitleColor = OxyColors.White,
            PlotAreaBorderColor = OxyColors.Gray,
           
        };

        // oś kategorii (etykiety dni) — przypisz Key
        var categoryAxis = new CategoryAxis
        {
            Key = "CategoryAxis",
            Position = AxisPosition.Bottom,
            Title = "Time",
            IsZoomEnabled = false,
            IsPanEnabled = false
        };

        // oś wartości — przypisz Key
        var valueAxis = new LinearAxis
        {
            Key = "ValueAxis",
            Position = AxisPosition.Left,
            Title = "Number of visits",
            Minimum = 0,
            IsZoomEnabled = false,
            IsPanEnabled = false
        };

        // BarSeries - ale "transponujemy" ją przez powiązanie kluczy osi
        var series = new BarSeries
        {
            // YAxisKey wskazuje na Key osi kategorii, XAxisKey na Key osi wartości
            YAxisKey = categoryAxis.Key,
            XAxisKey = valueAxis.Key,
            FillColor = OxyColors.LimeGreen,
            TrackerFormatString = "{1}: {2:0}" // tooltip: kategoria i wartość jako int
        };

        // Dodaj etykiety i wartości (BarItem)
        foreach (var point in Points)
        {
            categoryAxis.Labels.Add(point.Date.ToString("dd.MM"));
            series.Items.Add(new BarItem(point.VisitsNumber));
        }

        VisitsModel.Axes.Add(categoryAxis);
        VisitsModel.Axes.Add(valueAxis);
        VisitsModel.Series.Add(series);

        VisitsModel.InvalidatePlot(true);
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
