using GymManagementSystem.WPF.ViewModels.ScheduledClass;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.ScheduledClass;

public partial class ScheduledClassDetailsView : UserControl
{
    public ScheduledClassDetailsView()
    {

        Loaded += (_, _) =>
        {
            ((ScheduledClassDetailsViewModel)DataContext).LoadScheduledClassCommand.Execute(null);
        };
        InitializeComponent();
    }
}
