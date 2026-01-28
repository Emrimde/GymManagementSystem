using GymManagementSystem.WPF.ViewModels.ScheduledClass;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.ScheduledClass;
public partial class ScheduledClassView : UserControl
{
    public ScheduledClassView()
    {
        Loaded += (_, _) =>
        {
            ((ScheduledClassViewModel)DataContext).LoadScheduleClassesCommand.Execute(null);
        };
        InitializeComponent();
    }
}
