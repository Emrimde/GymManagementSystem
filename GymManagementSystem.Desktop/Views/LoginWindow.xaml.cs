using GymManagementSystem.Desktop.ViewModel;
using GymManagementSystem.Desktop.Views.UserControls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GymManagementSystem.Desktop.Views
{
    public partial class LoginWindow : Window
    {
     //private readonly IServiceProvider _serviceProvider;
        public LoginWindow()
        //IServiceProvider serviceProvider,
        //TestingViewModel testingViewModel

        {
            InitializeComponent();
            //_serviceProvider = serviceProvider;

            //testingViewModel.LoginSuccessful += () =>
            //{
            //    var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>(); // nie umiem tego zrobic inaczej
            //    // Pobieramy MainWindow z DI
            //    var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            //    mainWindow.DataContext = mainViewModel;
            //    var dashboard = _serviceProvider.GetRequiredService<Dashboard>();
            //    //mainViewModel.CurrentViewModel = dashboard;
            //    mainWindow.Show();

            //    this.Close();
            //};
        }
    }
}
