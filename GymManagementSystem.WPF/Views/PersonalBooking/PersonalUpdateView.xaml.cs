using GymManagementSystem.WPF.ViewModels.PersonalBooking;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.PersonalBooking;
public partial class PersonalUpdateView : UserControl
{
    public PersonalUpdateView()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            ((PersonalBookingUpdateViewModel)DataContext).LoadCommand.Execute(null);
        };
    }
}
