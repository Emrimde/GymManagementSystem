using GymManagementSystem.WPF.ViewModels.ClassBooking;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.ClassBooking;
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
