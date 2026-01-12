using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GymManagementSystem.WPF.ViewModels.Settings;

public class GeneralSettingsViewModel : ViewModel
{

    private readonly GeneralGymDetailsHttpClient _httpClient;
    private GeneralGymUpdateRequest _generalSettingsUpdateRequest;
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    public ICommand SaveGeneralSettingsCommand { get; }
    public GeneralGymUpdateRequest GeneralGymDetailsUpdateRequest
    {
        get { return _generalSettingsUpdateRequest; }
        set { _generalSettingsUpdateRequest = value; OnPropertyChanged(); }
    }

    public GeneralSettingsViewModel(SidebarViewModel sidebarVm,GeneralGymDetailsHttpClient httpClient, INavigationService navigationService)
    {
        SidebarView = sidebarVm;
        Navigation = navigationService;
        _httpClient = httpClient;
        GeneralGymDetailsUpdateRequest = new GeneralGymUpdateRequest();
        _ = LoadGeneralSettings();
        SaveGeneralSettingsCommand = new AsyncRelayCommand(UpdateGeneralSettings, item => true);
    }

    private async Task UpdateGeneralSettings(object arg)
    {
        Result<GeneralGymResponse> result = await _httpClient.PutGeneralSettingsAsync(GeneralGymDetailsUpdateRequest);
        if (result.IsSuccess)
        {
            MessageBox.Show($"General settings updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Resources["GymName"] = result.Value!.GymName;
            Application.Current.Resources["Address"] = result.Value!.Address;
            Application.Current.Resources["ContactNumber"] = result.Value!.ContactNumber;
            Application.Current.Resources["PrimaryColor"] = (SolidColorBrush)(new BrushConverter()).ConvertFromString(result.Value!.PrimaryColor)!; // czy to poprawne?
            Application.Current.Resources["SecondColor"] = (SolidColorBrush)(new BrushConverter()).ConvertFromString(result.Value!.SecondColor)!;
            Application.Current.Resources["BackgoundColor"] = (SolidColorBrush)(new BrushConverter()).ConvertFromString(result.Value!.BackgroundColor)!; ;
            Navigation.NavigateTo<DashboardViewModel>();
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}", "Error during edition", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private async Task LoadGeneralSettings()
    {
        GeneralGymResponse response = await _httpClient.GetGeneralGymSettingsAsync();
        GeneralGymUpdateRequest generalGymUpdate = response.ToGeneralGymUpdateRequest();
        GeneralGymDetailsUpdateRequest = generalGymUpdate;
    }
}
