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

namespace GymManagementSystem.WPF.Views.ScheduleWindows;

/// <summary>
/// Logika interakcji dla klasy AddChoiceDialog.xaml
/// </summary>
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
