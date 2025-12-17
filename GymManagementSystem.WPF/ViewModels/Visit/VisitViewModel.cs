using GymManagementSystem.Core.DTO;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;

namespace GymManagementSystem.WPF.ViewModels.Visit;

public class VisitViewModel : ViewModel, IParameterReceiver
{
    private readonly VisitHttpClient _visitHttpClient;
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private ObservableCollection<VisitResponse> _visits;

    public ObservableCollection<VisitResponse> Visits
    {
        get { return _visits; }
        set { _visits = value; OnPropertyChanged(); }
    }


    public VisitViewModel(VisitHttpClient visitHttpClient, INavigationService navigation,SidebarViewModel sidebarViewModel)
    {
        _visitHttpClient = visitHttpClient;
        Navigation = navigation;
        SidebarView = sidebarViewModel;
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            LoadVisits(clientId);
        }
    }

    private async Task LoadVisits(Guid clientId)
    {
        Result<ObservableCollection<VisitResponse>> result = await _visitHttpClient.GetAllClientVisitsAsync(clientId);
        if(result.IsSuccess)
        {
            Visits = result.Value!;
        }
        else
        {
           MessageBox.Show($"Error loading visits: {result.ErrorMessage}");
        }
    }
}
