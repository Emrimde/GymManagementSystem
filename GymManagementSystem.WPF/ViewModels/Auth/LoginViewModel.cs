using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Mappers;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.Services;
using GymManagementSystem.WPF.ViewModels.Dashboard;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Auth;

public class LoginViewModel : ViewModel
{
    public ICommand OpenRegisterViewCommand { get; }
    public ICommand LoginCommand { get; }

    private string email = string.Empty;
    public string Email
    {
        get { return email; }
        set
        {
            email = value; OnPropertyChanged();
        }
    }

    private string _password = string.Empty;
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value; OnPropertyChanged();
        }
    }

    public INavigationService Navigation { get; set; }

    private readonly AuthService _authService;
    private readonly AuthHttpClient _authHttpClient;
    public LoginViewModel(INavigationService navigationService, AuthHttpClient authHttpClient, AuthService authService)
    {
        _authService = authService;
        Navigation = navigationService;
        _authHttpClient = authHttpClient;
        OpenRegisterViewCommand = new RelayCommand(item => Navigation.NavigateTo<RegisterViewModel>(), item => true);
        LoginCommand = new AsyncRelayCommand(LoginAsync, item => Email != string.Empty && Password != string.Empty);
    }

    private async Task LoginAsync(object arg)
    {
        SignInDto signInDto = AuthMapper.ToSignInDto(Email, Password);
        Result<AuthenticationResponse> result = await _authHttpClient.LoginAsync(signInDto);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            _authService.SetProperty(result.Value!.Token!);
            if (result.Value!.MustChangePassword)
            {
                Navigation.NavigateTo<ChangePasswordViewModel>();
            }
            else
            {
                Navigation.NavigateTo<DashboardViewModel>();
            }
        }
    }
}
