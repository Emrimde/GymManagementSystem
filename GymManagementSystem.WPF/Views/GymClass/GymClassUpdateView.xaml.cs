using GymManagementSystem.WPF.ViewModels.GymClass;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.GymClass;
public partial class GymClassUpdateView : UserControl
{
    public GymClassUpdateView()
    {
        Loaded += (_, _) =>
        {
            ((GymClassUpdateViewModel)DataContext).LoadGymClassForEditCommand.Execute(null);
        };
        InitializeComponent();
    }
}
