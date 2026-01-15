using GymManagementSystem.WPF.ViewModels.Client;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Client;

/// <summary>
/// Logika interakcji dla klasy ClientUpdateView.xaml
/// </summary>
public partial class ClientUpdateView : UserControl
{
    public ClientUpdateView()
    {
        Loaded += (_, _) =>
        {
            ((ClientUpdateViewModel)DataContext).LoadClientCommand.Execute(null);
        };
        InitializeComponent();
    }
}
