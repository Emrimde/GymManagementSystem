using GymManagementSystem.WPF.ViewModels.ClientMembership;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.ClientMembership;
public partial class ClientMembershipDetailsView : UserControl
{
    public ClientMembershipDetailsView()
    {
        Loaded += (_, _) =>
        {
            ((ClientMembershipDetailsViewModel)DataContext).LoadClientMembershipCommand.Execute(null);
        };
        InitializeComponent();
    }
}
