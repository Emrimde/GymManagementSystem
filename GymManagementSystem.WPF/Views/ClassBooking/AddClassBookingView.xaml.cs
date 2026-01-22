using GymManagementSystem.WPF.ViewModels.ClassBooking;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GymManagementSystem.WPF.Views.ClassBooking
{
    /// <summary>
    /// Logika interakcji dla klasy AddClassBookingView.xaml
    /// </summary>
    public partial class AddClassBookingView : UserControl
    {
        public AddClassBookingView()
        {
            Loaded += (_, _) =>
            {
                ((AddClassBookingViewModel)DataContext).LoadDataForAddClassBookingCommand.Execute(null);
            };
            InitializeComponent();
        }
    }
}
