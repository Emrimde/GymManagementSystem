using GymManagementSystem.WPF.ViewModels.TrainerContract;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.TrainerContract;
public partial class TrainerContractDetailsView : UserControl
{
    public TrainerContractDetailsView()
    {
        Loaded += (_, _) =>
         {
           ((TrainerContractDetailsViewModel)DataContext).LoadTrainerCommand.Execute(null);
         };
        InitializeComponent();
    }
}
