using GymManagementSystem.WPF.ViewModels.Client;
using GymManagementSystem.WPF.ViewModels.Staff;
using GymManagementSystem.WPF.Views.Staff;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Staff;

/// <summary>
/// Logika interakcji dla klasy ClientUpdateView.xaml
/// </summary>
public partial class StaffUpdateView : UserControl
{
    public StaffUpdateView()
    {
        Loaded += (_, _) =>
        {
            ((StaffUpdateViewModel)DataContext).LoadPersonCommand.Execute(null);
        };
        InitializeComponent();
    }
}
