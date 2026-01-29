using GymManagementSystem.WPF.ViewModels.Staff;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Staff;
public partial class StaffDetailsView : UserControl
{
    public StaffDetailsView()
    {
        Loaded += (_, _) =>
        {
            ((StaffDetailsViewModel)DataContext).LoadPersonDetailsCommand.Execute(null);
        };
        InitializeComponent();
    }
}
