using System.Windows;

namespace GymManagementSystem.WPF.Views.ScheduleWindows;
public partial class AddChoiceDialog : Window
{
    public string Choice { get; private set; } = "";

    public AddChoiceDialog()
    {
        InitializeComponent();
    }

    private void TimeOff_Click(object sender, RoutedEventArgs e)
    {
        Choice = "TimeOff";
        DialogResult = true;
    }

    private void Booking_Click(object sender, RoutedEventArgs e)
    {
        Choice = "Booking";
        DialogResult = true;
    }
}
