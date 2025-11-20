
using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Desktop.Services;
using GymManagementSystem.Desktop.Mappers;
using GymManagementSystem.Desktop.Views.UserControls;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore.Metadata;
using GymManagementSystem.Desktop.ServiceContracts;

namespace GymManagementSystem.Desktop.ViewModel;

public class LoginViewModel : ViewModelBase
{
    public ICommand LoginCommand { get; }
    public ICommand OpenRegisterViewCommand { get; }
    private readonly AuthHtppClient _authService;
    private readonly INavigationService _navigationService;
    public LoginViewModel(AuthHtppClient authService, INavigationService navigationService)
    {
        _navigationService = navigationService;
        _authService = authService;
        LoginCommand = new AsyncRelayCommand(LoginAsync);
        OpenRegisterViewCommand = new RelayCommand(OpenRegisterView);
    }

    private void OpenRegisterView(object? arg)
    {
        _navigationService.NavigateView<RegisterViewModel>();
        //_mainViewModel.CurrentView = new RegisterView();
        //_mainViewModel.CurrentView.DataContext = new RegisterViewModel(_authService);
    }

    private string username = string.Empty;
    public string Username
    {
        get => username;
        set { username = value; OnPropertyChanged(); }
    }

    private string password = string.Empty;


    public string Password
    {
        get { return password; }
        set
        {
            password = value;
            OnPropertyChanged();
        }
    }

    private async Task LoginAsync(object parameter)
    {

        try
        {
            SignInDto signInDto = AuthMapper.ToSignInDto(Username, Password);
            HttpResponseMessage response = await _authService.LoginAsync(signInDto);

            if (response.IsSuccessStatusCode)
            {
                //_navigationService.NavigateView<Dashboard>();
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Login failed: {error}");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Connection error: {ex.Message}");
        }
    }
}
