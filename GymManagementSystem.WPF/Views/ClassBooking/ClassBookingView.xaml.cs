using GymManagementSystem.WPF.ViewModels.ClassBooking;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.ClassBooking;
public partial class ClassBookingView : UserControl
{
    public ClassBookingView()
    {
        Loaded += (_, _) =>
        {
            ((ClassBookingViewModel)DataContext).LoadClassBookingDataCommand.Execute(null);
        };
        InitializeComponent();
    }
}
