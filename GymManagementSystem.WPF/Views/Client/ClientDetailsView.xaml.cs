using GymManagementSystem.WPF.ViewModels.Client;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Client;
public partial class ClientDetailsView : UserControl
{
    public ClientDetailsView()
    {
        Loaded += (_, _) =>
        {
            ((ClientDetailsViewModel)DataContext).LoadClientDetailsCommand.Execute(null);
        };
        InitializeComponent();
    }
}
