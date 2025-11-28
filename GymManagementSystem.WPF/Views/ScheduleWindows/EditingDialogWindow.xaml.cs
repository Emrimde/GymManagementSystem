using GymManagementSystem.WPF.ViewModels.ScheduleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GymManagementSystem.WPF.Views.ScheduleWindows
{
    /// <summary>
    /// Logika interakcji dla klasy EditingDialogWindow.xaml
    /// </summary>
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
}
