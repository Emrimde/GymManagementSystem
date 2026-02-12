using GymManagementSystem.WPF.ViewModels.TrainerRate;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.TrainerRate;
public partial class TrainerRateView : UserControl
{
    public TrainerRateView()
    {
        Loaded += (_, _) =>
        {
            ((TrainerRateViewModel)DataContext).LoadTrainerRatesCommand.Execute(null);
        };
        InitializeComponent();
    }
}
