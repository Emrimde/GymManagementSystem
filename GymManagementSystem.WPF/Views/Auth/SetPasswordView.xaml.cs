using GymManagementSystem.WPF.ViewModels.Auth;
using System.Windows;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Auth;

public partial class SetPasswordView : UserControl
{
    public SetPasswordView()
    {
        InitializeComponent();
    }

    private void NewPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is SetNewPasswordViewModel vm)
            vm.NewPassword = ((PasswordBox)sender).Password;
    }

    private void ConfirmPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is SetNewPasswordViewModel vm)
            vm.ConfirmPassword = ((PasswordBox)sender).Password;
    }

}
