using GymManagementSystem.WPF.ViewModels.Employee;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Employee;
public partial class EmployeeDetailsView : UserControl
{
    public EmployeeDetailsView()
    {
        Loaded += (_, _) =>
        {
            ((EmployeeDetailsViewModel)DataContext).LoadEmployeeCommand.Execute(null);
        };
        InitializeComponent();
    }
}
