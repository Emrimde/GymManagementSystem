using GymManagementSystem.WPF.ViewModels.Staff;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Staff;
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
