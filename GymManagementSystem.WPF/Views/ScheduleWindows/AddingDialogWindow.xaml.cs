using System.Windows;
using GymManagementSystem.WPF.ViewModels.ScheduleViewModels;

namespace GymManagementSystem.WPF.Views.ScheduleWindows;

public partial class AddingDialogWindow : Window
{
    public AddingDialogWindow()
    {
        InitializeComponent();

        Loaded += (s, e) =>
        {
            if (DataContext is AddingDialogWindowViewModel vm)
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
