using GymManagementSystem.WPF.ViewModels.Client;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Client;

/// <summary>
/// Logika interakcji dla klasy ClientView.xaml
/// </summary>
public partial class ClientView : UserControl
{
    public ClientView()
    {
        Loaded += (_,_) =>
        ((ClientViewModel)DataContext).LoadClientsCommand.Execute(null);
        InitializeComponent();
    }
}
