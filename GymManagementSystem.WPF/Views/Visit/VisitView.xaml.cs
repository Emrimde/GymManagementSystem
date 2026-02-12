using GymManagementSystem.WPF.ViewModels.Visit;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Visit;
public partial class VisitView : UserControl
{
    public VisitView()
    {
        Loaded += (_, _) => ((VisitViewModel)DataContext).LoadVisitViewDataCommand.Execute(null);
        InitializeComponent();
    }
}
