using GymManagementSystem.WPF.ViewModels.MembershipFeature;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.MembershipFeature;

/// <summary>
/// Logika interakcji dla klasy MembershipFeatureUpdateView.xaml
/// </summary>
public partial class MembershipFeatureUpdateView : UserControl
{
    public MembershipFeatureUpdateView()
    {
        Loaded += (_, _) =>
        {
            ((MembershipFeatureUpdateViewModel)DataContext).LoadMembershipFeatureCommand.Execute(null);
        };
        InitializeComponent();
    }
}
