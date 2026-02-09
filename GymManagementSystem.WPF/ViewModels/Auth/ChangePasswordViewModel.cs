using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Dashboard;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Auth;

public class ChangePasswordViewModel : ViewModel
{
    private string _newPassword = string.Empty;
    public string NewPassword
    {
        get => _newPassword;
        set { _newPassword = value; OnPropertyChanged(); }
    }

    private string _confirmPassword = string.Empty;
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set { _confirmPassword = value; OnPropertyChanged(); }
    }

    public ICommand ChangePasswordCommand { get; }
    public ICommand CancelCommand { get; }

    private readonly AuthHttpClient _authHttpClient;
    private readonly INavigationService _navigation;

    public ChangePasswordViewModel(
        AuthHttpClient authHttpClient,
        INavigationService navigation)
    {
        _authHttpClient = authHttpClient;
        _navigation = navigation;
        CancelCommand = new RelayCommand(item => navigation.NavigateTo<LoginViewModel>(), item => true);
        ChangePasswordCommand = new AsyncRelayCommand(ChangePasswordAsync);
    }

    private async Task ChangePasswordAsync(object arg)
    {
        if (string.IsNullOrWhiteSpace(NewPassword))
        {
            MessageBox.Show("Password cannot be empty");
            return;
        }

        if (NewPassword != ConfirmPassword)
        {
            MessageBox.Show("Passwords do not match");
            return;
        }

        ForceChangePasswordRequest dto = new()
        {
            NewPassword = NewPassword
        };

        Result<Unit> result = await _authHttpClient.ForceChangePasswordAsync(dto);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.GetUserMessage(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        _navigation.NavigateTo<DashboardViewModel>();
    }
}
