using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Mappers;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.Services;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels;

public class LoginViewModel : ViewModel
{
    public ICommand OpenRegisterViewCommand { get; }
    public ICommand LoginCommand { get; }

    private string _username;
    public string Username
    {
        get { return _username; }
        set
        {
            _username = value; OnPropertyChanged();
        }
    }

    private string _password;
    public string Password
    {
        get { return _password; }
        set
        {
            _password = value; OnPropertyChanged();
        }
    }

    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }
    private readonly AuthService _authService;
    private readonly AuthHttpClient _authHttpClient;
    public LoginViewModel(INavigationService navigationService, AuthHttpClient authHttpClient,AuthService authService)
    {
        _authService = authService;
        _navigation = navigationService;
        _authHttpClient = authHttpClient;   
        OpenRegisterViewCommand = new RelayCommand(o => Navigation.NavigateTo<RegisterViewModel>(), o => true);
        LoginCommand = new AsyncRelayCommand(LoginAsync);
    }

    private async Task LoginAsync(object arg)
    {
        SignInDto signInDto = AuthMapper.ToSignInDto(Username,Password);
        Result<AuthenticationResponse> result = await _authHttpClient.LoginAsync(signInDto);
        if (!result.IsSuccess)
        {
            MessageBox.Show("Login failed. Please check your credentials and try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            _authService.SetProperty(result.Value!.Token!); // tutaj

            _navigation.NavigateTo<DashboardViewModel>();
        }
    }
}
