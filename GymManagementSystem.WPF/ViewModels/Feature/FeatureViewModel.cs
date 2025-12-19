using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using System.Collections.ObjectModel;
using System.Windows;

namespace GymManagementSystem.WPF.ViewModels.Feature;
public class FeatureViewModel : ViewModel
{
	private ObservableCollection<FeatureResponse> _features;

	public ObservableCollection<FeatureResponse> Features
	{
		get { return _features; }
		set { _features = value; OnPropertyChanged(); }
	}

	private readonly FeatureHttpClient _featureHttpClient;
public FeatureViewModel(FeatureHttpClient featureHttpClient, SidebarViewModel sidebarView)
    {
        _featureHttpClient = featureHttpClient;
        SidebarView = sidebarView;
		Features = new ObservableCollection<FeatureResponse>();
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
