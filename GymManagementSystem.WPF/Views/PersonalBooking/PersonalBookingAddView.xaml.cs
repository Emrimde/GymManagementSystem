using GymManagementSystem.WPF.ViewModels.PersonalBooking;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.PersonalBooking;
public partial class PersonalBookingAddView : UserControl
{
    public PersonalBookingAddView()
    {
        Loaded += (_,_) => {
            ((PersonalBookingAddViewModel)DataContext).LoadPersonalTrainersCommand.Execute(this);    
        };
        InitializeComponent();
    }
}
