using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels
{
    public class DashboardViewModel : ViewModel
    {
        public SidebarViewModel SidebarView { get; }
        public ICommand OpenRegisterViewCommand { get; }
        private INavigationService _navigation;
        public INavigationService Navigation
        {
            get { return _navigation; }
            set
            {
                _navigation = value; OnPropertyChanged();
            }
        }

        public DashboardViewModel(INavigationService navigationService, SidebarViewModel sidebarViewModel)
        {
            _navigation = navigationService;
            OpenRegisterViewCommand = new RelayCommand(o => Navigation.NavigateTo<RegisterViewModel>(), o => true);
            SidebarView = sidebarViewModel;
        }

    }
}
