using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Feature;
public class FeatureViewModel : ViewModel
{
	private ObservableCollection<FeatureResponse> _features;
    public INavigationService Navigation { get; set; }
	public ObservableCollection<FeatureResponse> Features
	{
		get { return _features; }
		set { _features = value; OnPropertyChanged(); }
	}

	private readonly FeatureHttpClient _featureHttpClient;
	public ICommand OpenFeatureAddViewCommand { get;  }
public FeatureViewModel(FeatureHttpClient featureHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _featureHttpClient = featureHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
		Features = new ObservableCollection<FeatureResponse>();
        OpenFeatureAddViewCommand = new RelayCommand(item => Navigation.NavigateTo<FeatureAddViewModel>(), item => true);
        _ = LoadFeaturesAsync();
    }

    private async Task LoadFeaturesAsync()
    {
        Result<ObservableCollection<FeatureResponse>> result = await _featureHttpClient.GetFeaturesForSelect();
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
            return;
        }

        Features = result.Value!;
    }

    public SidebarViewModel SidebarView { get; set; }



}
