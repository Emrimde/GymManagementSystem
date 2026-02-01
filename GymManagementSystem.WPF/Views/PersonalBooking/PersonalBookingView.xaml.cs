using GymManagementSystem.WPF.ViewModels.PersonalBooking;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.PersonalBooking;

public partial class PersonalBookingView : UserControl
{
    public PersonalBookingView()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            ((PersonalBookingViewModel)DataContext).LoadPersonalBookingCommand.Execute(null);
        };
    }
}
