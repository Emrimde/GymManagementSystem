using GymManagementSystem.WPF.ViewModels.Staff;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Staff;
public partial class StaffView : UserControl
{
    public StaffView()
    {
        Loaded += (_, _) =>
        {
            ((StaffViewModel)DataContext).LoadPeopleCommand.Execute(null);
        };
        InitializeComponent();
    }
}
