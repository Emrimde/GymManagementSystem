using GymManagementSystem.WPF.ViewModels.Membership;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.Membership;

public partial class MembershipEditView : UserControl
{
    public MembershipEditView()
    {
        Loaded += (_, _) =>
        {
            ((MembershipEditViewModel)DataContext).LoadMembershipCommand.Execute(null);
        };
        InitializeComponent();
    }
}
