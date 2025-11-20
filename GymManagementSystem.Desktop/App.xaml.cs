using GymManagementSystem.Desktop.ServiceContracts;
using GymManagementSystem.Desktop.Services;
using GymManagementSystem.Desktop.ViewModel;
using GymManagementSystem.Desktop.Views;
using GymManagementSystem.Desktop.Views.UserControls;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GymManagementSystem.Desktop;

public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        // Rejestracja HttpClient
        services.AddHttpClient<AuthHtppClient>(options =>
        {
            options.BaseAddress = new Uri("http://localhost:5105/api/auth/");
            options.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        // rejestracja ViewModel i okien
        //services.AddSingleton<MainViewModel>();
        services.AddSingleton<NavigationService>();

        services.AddTransient<RegisterViewModel>();
        services.AddTransient<LoginViewModel>();
        services.AddSingleton<MainWindow>();
        services.AddTransient<RegisterView>();
        services.AddTransient<Clients>();
        services.AddSingleton<SideBarViewModel>();
        services.AddTransient<LoginWindow>();
        services.AddTransient<Dashboard>();
        services.AddSingleton<TestingViewModel>();
        services.AddScoped<INavigationService, NavigationService>();

        ServiceProvider = services.BuildServiceProvider();
        var mainViewModel = ServiceProvider.GetRequiredService<TestingViewModel>();
        var navigationService = ServiceProvider.GetRequiredService<INavigationService>() as NavigationService;

        // navigationService?.SetMainViewModel(ServiceProvider.GetRequiredService<MainViewModel>());

        var startupWindow = ServiceProvider.GetRequiredService<LoginWindow>();
        startupWindow.DataContext = mainViewModel;
        startupWindow.Show();
    }
}
