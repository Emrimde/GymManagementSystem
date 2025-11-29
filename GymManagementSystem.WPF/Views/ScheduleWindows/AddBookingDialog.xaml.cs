using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.WPF.ViewModels.ScheduleViewModels;
using System.Windows;
using System.Windows.Controls;

namespace GymManagementSystem.WPF.Views.ScheduleWindows
{
    /// <summary>
    /// Logika interakcji dla klasy AddBookingDialog.xaml
    /// </summary>
    public partial class AddBookingDialog : Window
    {
        public AddBookingDialog()
        {
            InitializeComponent();
            Loaded += (s, e) =>
            {
                if (DataContext is AddBookingDialogViewModel vm)
                {
                    vm.CloseRequested += result =>
                    {
                        DialogResult = result;
                        Close();
                    };
                }
            };
        }

        private void AutoCompleteBox_TextChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AddBookingDialogViewModel vm &&
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
            if (DataContext is AddBookingDialogViewModel vm &&
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
