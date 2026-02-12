using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Mappers;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Auth;
public class RegisterViewModel : ViewModel
{
    public ICommand OpenLoginViewCommand { get; }
    private readonly AuthHttpClient _authHttpClient;
    public ICommand RegisterCommand { get; }
    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }

    private string _email = string.Empty;

    public  string Email
    {
        get { return _email ; }
        set { _email = value;  OnPropertyChanged(); }
    }

    private string _password = string.Empty;

    public string Password
        {
        get { return _password; }
        set { _password = value; OnPropertyChanged(); }
    }

    private string _confirmPassword = string.Empty;
    public string ConfirmPassword
    {
        get { return _confirmPassword; }
        set { _confirmPassword = value; OnPropertyChanged(); }
    }

    private string _username = string.Empty;
    public string Username
    {
        get { return _username; }
        set { _username = value; OnPropertyChanged(); }
    }



    public RegisterViewModel(INavigationService navigationService, AuthHttpClient authHttpClient)
    {
        _navigation = navigationService;
        OpenLoginViewCommand = new RelayCommand(o => Navigation.NavigateTo<LoginViewModel>(), o => true);
        RegisterCommand = new AsyncRelayCommand(RegisterAsync);
        _authHttpClient = authHttpClient;
    }

    private async Task RegisterAsync(object arg)
    {
        RegisterDto registerDto = AuthMapper.ToRegisterDto(this);
        Result<Unit> result =  await _authHttpClient.RegisterAsync(registerDto);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Registration failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
}
