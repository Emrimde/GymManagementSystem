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
    private PlotModel clientMemberships;

    public PlotModel ClientMemberships
    {
        get { return clientMemberships; }
        set { clientMemberships = value; OnPropertyChanged(); }
    }

    private readonly DashboardHttpClient _dashboardHttpClient;

    private DashboardKpiResponse _dashboardKpi;

    public DashboardKpiResponse DashboardKPI
    {
        get { return _dashboardKpi; }
        set { _dashboardKpi = value; OnPropertyChanged(); }
    }
    private DashboardPlotsDataResponse _points;

    public DashboardPlotsDataResponse Points
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
        Result<DashboardPlotsDataResponse> result =
            await _dashboardHttpClient.GetAllPointsAsync();

        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
            return;
        }

        Points = result.Value!;

        VisitsModel = CreateBarChart(
            "Visits over week",
            "Number of visits",
            Points.VisitsPoints
        );

        ClientMemberships = CreateBarChart(
            "Memberships bought over the week",
            "Number of bought memberships",
            Points.ClientMembershipsPoints
        );
    }

    private PlotModel CreateBarChart(
      string title,
      string yAxisTitle,
      IEnumerable<PointResponse> points)
    {
        var model = new PlotModel
        {
            Title = title,
            Background = OxyColors.Transparent,
            TextColor = OxyColors.White,
            TitleColor = OxyColors.White,
            PlotAreaBorderColor = OxyColors.Gray
        };

        var categoryAxis = new CategoryAxis
        {
            Key = "CategoryAxis",
            Position = AxisPosition.Bottom,
            Title = "Time",
            IsZoomEnabled = false,
            IsPanEnabled = false
        };

        var valueAxis = new LinearAxis
        {
            Key = "ValueAxis",
            Position = AxisPosition.Left,
            Title = yAxisTitle,
            Minimum = 0,
            IsZoomEnabled = false,
            IsPanEnabled = false
        };

        var series = new BarSeries
        {
            YAxisKey = categoryAxis.Key,
            XAxisKey = valueAxis.Key,
            FillColor = OxyColors.LimeGreen,
            TrackerFormatString = "{1}: {2:0}"
        };

        foreach (var point in points)
        {
            categoryAxis.Labels.Add(point.Date.ToString("dd.MM"));
            series.Items.Add(new BarItem(point.TimeSeriesPoint));
        }

        model.Axes.Add(categoryAxis);
        model.Axes.Add(valueAxis);
        model.Series.Add(series);
        model.InvalidatePlot(true);

        return model;
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
