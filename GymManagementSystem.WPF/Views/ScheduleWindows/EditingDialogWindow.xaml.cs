using GymManagementSystem.WPF.ViewModels.ScheduleViewModels;
using System.Windows;

namespace GymManagementSystem.WPF.Views.ScheduleWindows;
public partial class EditingDialogWindow : Window
{
    public EditingDialogWindow()
    {
        InitializeComponent();
        Loaded += (s, e) =>
        {
            if (DataContext is EditingDialogWindowViewModel vm)
            {
                vm.CloseRequested += result =>
                {
                    DialogResult = result;
                    Close();
                };
            }
        };
    }
}
