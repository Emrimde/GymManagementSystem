using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Desktop.Services;
using GymManagementSystem.Desktop.Mappers;
using GymManagementSystem.Desktop.ServiceContracts;
using GymManagementSystem.Desktop.Views.UserControls;
using System.Net.Http;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.Desktop.ViewModel;

public class TestingViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    //public event Action? LoginSuccessful;
    private readonly AuthHtppClient _authService;
    public ICommand LoginCommand { get; }
    public TestingViewModel(AuthHtppClient authService,INavigationService navigationService)
    {
        _navigationService = navigationService;
        _authService = authService;
        LoginCommand = new AsyncRelayCommand(LoginAsync);
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
                // jak przekierowac do mainwindow.xaml??? 
                //LoginSuccessful?.Invoke();
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
