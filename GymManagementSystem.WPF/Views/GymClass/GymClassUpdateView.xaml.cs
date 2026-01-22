using GymManagementSystem.WPF.ViewModels.Client;
using GymManagementSystem.WPF.ViewModels.GymClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GymManagementSystem.WPF.Views.GymClass
{
    /// <summary>
    /// Logika interakcji dla klasy GymClassUpdateView.xaml
    /// </summary>
    public partial class GymClassUpdateView : UserControl
    {
        public GymClassUpdateView()
        {
            Loaded += (_, _) =>
            {
                ((GymClassUpdateViewModel)DataContext).LoadGymClassForEditCommand.Execute(null);
            };
            InitializeComponent();
        }
    }
}
