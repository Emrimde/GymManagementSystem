using GymManagementSystem.WPF.ViewModels.GymClass;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.GymClass;
public partial class GymClassView : UserControl
{
    public GymClassView()
    {
        Loaded += (_, _) =>
        {
            ((GymClassViewModel)DataContext).LoadGymClassesCommand.Execute(null);
        };
        InitializeComponent();
    }
}
