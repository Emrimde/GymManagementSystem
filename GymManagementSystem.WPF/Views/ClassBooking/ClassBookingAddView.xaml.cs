using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.WPF.ViewModels.ClassBooking;
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

namespace GymManagementSystem.WPF.Views.ClassBooking
{
    /// <summary>
    /// Logika interakcji dla klasy ClassBookingAddView.xaml
    /// </summary>
    public partial class ClassBookingAddView : UserControl
    {
        public ClassBookingAddView()
        {
            InitializeComponent();
        }

        private void AutoCompleteBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ClassBookingAddViewModel vm &&
                sender is AutoCompleteBox acb)
            {
                // jeśli user edytuje tekst po wybraniu klienta → reset Selected
                if (vm.SelectedClient != null && acb.Text != vm.SelectedClient.FullName)
                {
                    vm.SelectedClient = null;
                }

                // dopiero wtedy triggerujemy wyszukiwanie
                vm.SearchQuery = acb.Text;
            }
        }

        private void AutoCompleteBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is ClassBookingAddViewModel vm &&
                sender is AutoCompleteBox acb &&
                acb.SelectedItem is ClientInfoResponse selected)
            {
                // ustawiamy wybranego klienta
                vm.SelectedClient = selected;

                // po wyborze klienta, wpisujemy jego nazwę i nie szukamy dalej
                vm.SearchQuery = selected.FullName;
            }
        }
    }
}
