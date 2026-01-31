using GymManagementSystem.WPF.ViewModels.MembershipPrice;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.MembershipPrice;

public partial class MembershipPriceView : UserControl
{
    public MembershipPriceView()
    {
        Loaded += (_, _) =>
        {
            ((MembershipPriceViewModel)DataContext).LoadMembershipPricesCommand.Execute(null);
        };
        InitializeComponent();
    }
}
