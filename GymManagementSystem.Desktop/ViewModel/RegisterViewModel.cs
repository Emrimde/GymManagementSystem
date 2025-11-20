using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Desktop.Mappers;
using GymManagementSystem.Desktop.ServiceContracts;
using GymManagementSystem.Desktop.Services;
using GymManagementSystem.Desktop.Views.UserControls;
using System.Net.Http;
using System.Windows.Input;

namespace GymManagementSystem.Desktop.ViewModel;

public class RegisterViewModel : ViewModelBase
{
    private readonly AuthHtppClient _authService;
    public ICommand OpenLoginViewCommand { get; }
    private readonly INavigationService _navigationService;
    public RegisterViewModel(AuthHtppClient authService, INavigationService navigationService)
    {
        _authService = authService;
        RegisterCommand = new AsyncRelayCommand(RegisterAsync);
        OpenLoginViewCommand = new RelayCommand(OpenLoginView);
        _navigationService = navigationService;
    }

    private void OpenLoginView(object? obj)
    {
        _navigationService.NavigateView<LoginViewModel>();
    }

    private async Task RegisterAsync(object parameter)
    {
        RegisterDto registerDto = AuthMapper.ToRegisterDto(this);
        HttpResponseMessage response = await _authService.RegisterAsync(registerDto);
        if (response.IsSuccessStatusCode)
        {
            _navigationService.NavigateView<LoginViewModel>();
        }
    }

    public ICommand RegisterCommand { get; set; }
    private string? email;

    public string? Email
    {
        get { return email; }
        set
        {
            email = value;
            OnPropertyChanged();
        }
    }

    private string? username;

    public string? Username
    {
         get { return username; }
        set
        {
            username = value;
            OnPropertyChanged();
        }
    }
    private string? password;

    public string? Password
    {
        get { return password; }
        set
        {
            password = value;
            OnPropertyChanged();
        }
    }
    private string? confirmPassword;

    public string? ConfirmPassword
    {
        get { return confirmPassword; }
        set
        {
            confirmPassword = value;
            OnPropertyChanged();
        }
    }



}
