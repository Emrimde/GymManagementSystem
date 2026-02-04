using GymManagementSystem.WPF.ViewModels.Auth;
using System.Windows;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Auth;
public partial class ChangePasswordView : UserControl
{
    public ChangePasswordView()
    {
        InitializeComponent();
    }
    private void NewPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is ChangePasswordViewModel vm)
            vm.NewPassword = ((PasswordBox)sender).Password;
    }

    private void ConfirmPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is ChangePasswordViewModel vm)
            vm.ConfirmPassword = ((PasswordBox)sender).Password;
    }
}
