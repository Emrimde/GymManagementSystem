using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Dashboard;
using Microsoft.Win32;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GymManagementSystem.WPF.ViewModels.Settings;

public class GeneralSettingsViewModel : ViewModel
{

    private readonly GeneralGymDetailsHttpClient _httpClient;
    private GeneralGymUpdateRequest _generalSettingsUpdateRequest = new();
    public GeneralGymUpdateRequest GeneralGymDetailsUpdateRequest
    {
        get { return _generalSettingsUpdateRequest; }
        set { _generalSettingsUpdateRequest = value; OnPropertyChanged(); }
    }
    public SidebarViewModel SidebarView { get; }
    public INavigationService Navigation { get; set; }

    public ICommand SaveGeneralSettingsCommand { get; }
    public ICommand PickLogoCommand { get; }
    public ImageSource? LogoPreview { get; set; }


    public GeneralSettingsViewModel(SidebarViewModel sidebarVm,GeneralGymDetailsHttpClient httpClient, INavigationService navigationService)
    {
        PickLogoCommand = new AsyncRelayCommand(item => PickLogo(), item => true);
        SidebarView = sidebarVm;
        Navigation = navigationService;
        _httpClient = httpClient;
        GeneralGymDetailsUpdateRequest = new GeneralGymUpdateRequest();
        _ = LoadGeneralSettings();
        SaveGeneralSettingsCommand = new AsyncRelayCommand(UpdateGeneralSettings, item => true);
    }

    private async Task PickLogo()
    {
        OpenFileDialog dialog = new OpenFileDialog() { Filter = "Images|*.png;*.jpg;"};
         if (dialog.ShowDialog() != true)
         {
             return;
         }
         LogoPreview = new BitmapImage(new Uri(dialog.FileName));
         OnPropertyChanged(nameof(LogoPreview));
         using var fs = System.IO.File.OpenRead(dialog.FileName);
         var content = new MultipartFormDataContent();
         content.Add(new StreamContent(fs), "file", Path.GetFileName(dialog.FileName));
        Result<string> result = await _httpClient.UploadLogoAsync(content);
        if(result.IsSuccess)
        {
            GeneralGymDetailsUpdateRequest.LogoUrl = result.Value!;
        }
        else
        {
            MessageBox.Show($"Error during logo upload: {result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
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
            Navigation.NavigateTo<DashboardViewModel>();
        }
        else
        {
            MessageBox.Show($"Error: {result.GetUserMessage()}", "Error during edition", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private async Task LoadGeneralSettings()
    {
        Result<GeneralGymResponse> response = await _httpClient.GetGeneralGymSettingsAsync();
        GeneralGymUpdateRequest generalGymUpdate = response.Value!.ToGeneralGymUpdateRequest();
        GeneralGymDetailsUpdateRequest = generalGymUpdate;
    }
}
