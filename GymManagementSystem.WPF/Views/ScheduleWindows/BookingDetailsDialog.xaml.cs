using GymManagementSystem.WPF.ViewModels.ScheduleViewModels;
using System.Windows;

namespace GymManagementSystem.WPF.Views.ScheduleWindows
{
    /// <summary>
    /// Logika interakcji dla klasy BookingDetailsDialog.xaml
    /// </summary>
    public partial class BookingDetailsDialog : Window
    {
        public BookingDetailsDialog()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                if (DataContext is BookingDetailsViewModel vm)
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
