using GymManagementSystem.Desktop.ViewModel;
using System.Windows;

namespace GymManagementSystem.Desktop;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }
}