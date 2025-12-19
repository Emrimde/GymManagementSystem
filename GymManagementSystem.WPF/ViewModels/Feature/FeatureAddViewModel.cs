using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Feature;

public class FeatureAddViewModel : ViewModel
{
    private ObservableCollection<FeatureResponse> _features;
    public ICommand AddFeatureCommand { get; set; }
    public INavigationService Navigation { get; set; }

    public FeatureAddRequest FeatureAdd {  get; set; }
    public ObservableCollection<FeatureResponse> Features
    {
        get { return _features; }
        set { _features = value; OnPropertyChanged(); }
    }
    public SidebarViewModel SidebarView { get; set; }

    private readonly FeatureHttpClient _featureHttpClient;
    public FeatureAddViewModel(FeatureHttpClient featureHttpClient, SidebarViewModel sidebarView, INavigationService _navigation)
    {
        _featureHttpClient = featureHttpClient;
        SidebarView = sidebarView;
        Features = new ObservableCollection<FeatureResponse>();
        Navigation = _navigation;
        AddFeatureCommand = new AsyncRelayCommand(item => AddFeatureAsync(), item => true);
    }

    private async Task AddFeatureAsync()
    {
        Result<Unit> result = await _featureHttpClient.PostFeatureAsync(FeatureAdd);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
            return;
        }
        Navigation.NavigateTo<FeatureViewModel>();
    }
}
