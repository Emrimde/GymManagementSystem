using GymManagementSystem.WPF.ViewModels.ClientMembership;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.ClientMembership;
public partial class ClientMembershipView : UserControl
{
    public ClientMembershipView()
    {
        Loaded += (_, _) =>
        {
            ((ClientMembershipViewModel)DataContext).LoadClientMembershipsDataCommand.Execute(null);
        };
        InitializeComponent();
    }
}
