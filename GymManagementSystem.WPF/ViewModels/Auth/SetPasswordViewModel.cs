using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Dashboard;
using GymManagementSystem.WPF.ViewModels.Staff;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Auth;
public class SetNewPasswordViewModel : ViewModel, IParameterReceiver
{
    private readonly AuthHttpClient _authService;

    private string _newPassword = string.Empty;
    private bool _isBusy;
    private string? _error;
    public INavigationService Navigation { get; }
    public SetNewPasswordViewModel(AuthHttpClient authService, INavigationService navigation)
    {
        _authService = authService;
        Navigation = navigation;
        SetPasswordCommand = new AsyncRelayCommand(item => SetPasswordAsync(), item => CanSetPassword());
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<StaffViewModel>(), item => true);
    }

    public string NewPassword
    {
        get => _newPassword;
        set
        {
            _newPassword = value;
            OnPropertyChanged();
            ((AsyncRelayCommand)SetPasswordCommand).RaiseCanExecuteChanged();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }
    private string _confirmPassword = string.Empty;

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set
        {
            _confirmPassword = value;
            OnPropertyChanged();
            ((AsyncRelayCommand)SetPasswordCommand).RaiseCanExecuteChanged();
        }
    }
    public string? Error
    {
        get => _error;
        set
        {
            _error = value;
            OnPropertyChanged();
        }
    }

    public ICommand SetPasswordCommand { get; }
    public ICommand CancelCommand { get; }

    private bool CanSetPassword()
     => !IsBusy
        && !string.IsNullOrWhiteSpace(NewPassword)
        && !string.IsNullOrWhiteSpace(ConfirmPassword)
        && NewPassword == ConfirmPassword;

    private async Task SetPasswordAsync()
    {
        IsBusy = true;
        Error = null;

        SetNewPasswordRequest request = new SetNewPasswordRequest
        {
            NewPassword = NewPassword,
            PersonId = _personId
        };

        Result<Unit> result = await _authService.SetNewPasswordAsync(request);

        if (!result.IsSuccess)
        {
            Error = result.GetUserMessage();
            MessageBox.Show($"{Error}");
        }
        else
        {
            Navigation.NavigateTo<DashboardViewModel>();
        }

        IsBusy = false;
    }
    private Guid _personId;
    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid personId)
        {
            _personId = personId;
        }
    }
}
