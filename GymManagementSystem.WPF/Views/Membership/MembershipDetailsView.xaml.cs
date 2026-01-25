using GymManagementSystem.WPF.ViewModels.Membership;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Membership;
public partial class MembershipDetailsView : UserControl
{
    public MembershipDetailsView()
    {
        Loaded += (_, _) =>
        {
            ((MembershipDetailsViewModel)DataContext).LoadMembershipDetailsCommand.Execute(null);
        };
        InitializeComponent();
    }
}
