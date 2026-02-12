using System.Windows;

namespace GymManagementSystem.WPF.Views.Visit.CustomDialogs;

public partial class FreeFriendArrivalDialog : Window
{
    public FreeFriendArrivalDialog()
    {
        InitializeComponent();
    }

    public string? InputText { get; private set; }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        InputText = InputBox.Text;
        DialogResult = true;
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

}
