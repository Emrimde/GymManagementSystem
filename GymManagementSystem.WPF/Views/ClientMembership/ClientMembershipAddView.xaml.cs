using GymManagementSystem.WPF.ViewModels.ClientMembership;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.ClientMembership
{
    /// <summary>
    /// Logika interakcji dla klasy ClientMembershipAddView.xaml
    /// </summary>
    public partial class ClientMembershipAddView : UserControl
    {
        public ClientMembershipAddView()
        {
            Loaded += (_, _) =>
            {
                ((ClientMembershipAddViewModel)DataContext).LoadClientMembershipViewData.Execute(null);
            };
            InitializeComponent();
        }
    }
}
