using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.Services;
using GymManagementSystem.WPF.ViewModels;
using GymManagementSystem.WPF.ViewModels.ClassBooking;
using GymManagementSystem.WPF.ViewModels.Client;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using GymManagementSystem.WPF.ViewModels.Contract;
using GymManagementSystem.WPF.ViewModels.Employee;
using GymManagementSystem.WPF.ViewModels.EmploymentTermination;
using GymManagementSystem.WPF.ViewModels.Feature;
using GymManagementSystem.WPF.ViewModels.GymClass;
using GymManagementSystem.WPF.ViewModels.Membership;
using GymManagementSystem.WPF.ViewModels.MembershipFeature;
using GymManagementSystem.WPF.ViewModels.MembershipPrice;
using GymManagementSystem.WPF.ViewModels.ScheduledClass;
using GymManagementSystem.WPF.ViewModels.Settings;
using GymManagementSystem.WPF.ViewModels.Staff;
using GymManagementSystem.WPF.ViewModels.Termination;
using GymManagementSystem.WPF.ViewModels.Trainer;
using GymManagementSystem.WPF.ViewModels.TrainerContract;
using GymManagementSystem.WPF.ViewModels.TrainerRate;
using GymManagementSystem.WPF.ViewModels.Visit;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Licensing;
using System.Windows;
using System.Windows.Media;

namespace GymManagementSystem.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App()
        {

            IServiceCollection services = new ServiceCollection();  
            services.AddSingleton(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>()
            });

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JFaF1cXGZCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdmWH1edHRWRWFdV0J/WEtWYEg=");
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<RegisterViewModel>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<ClientMembershipViewModel>();
            services.AddSingleton<SidebarViewModel>();
            services.AddTransient<ClientAddViewModel>();
            services.AddTransient<ClientMembershipAddViewModel>();
            services.AddTransient<ClientViewModel>();
            services.AddTransient<TerminationAddViewModel>();
            services.AddTransient<ContractDetailsViewModel>();
            services.AddTransient<MembershipViewModel>();
            services.AddTransient<ClientDetailsViewModel>();
            services.AddTransient<GeneralSettingsViewModel>();
            services.AddTransient<MembershipAddViewModel>();
            services.AddTransient<MembershipEditViewModel>();
            services.AddTransient<ClientUpdateViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddTransient<VisitViewModel>();
            services.AddTransient<TrainerScheduleViewModel>();
            services.AddTransient<GymClassAddViewModel>();
            services.AddTransient<GymClassViewModel>();
            services.AddTransient<ScheduledClassViewModel>();
            services.AddTransient<ScheduledClassDetailsViewModel>();
            services.AddTransient<ClassBookingAddViewModel>();
            services.AddTransient<ClassBookingViewModel>();
            services.AddTransient<EmployeeAddViewModel>();
            services.AddTransient<EmployeeDecisionViewModel>();
            services.AddTransient<TrainerContractAddViewModel>();
            services.AddTransient<TrainerContractDetailsViewModel>();
            services.AddTransient<TrainerRateViewModel>();
            services.AddTransient<TrainerRateAddViewModel>();
            services.AddTransient<EmploymentTerminationViewModel>();
            services.AddTransient<ClientMembershipDetailsViewModel>();
            services.AddTransient<MembershipDetailsViewModel>();
            services.AddTransient<MembershipPriceViewModel>();
            services.AddTransient<MembershipPriceAddViewModel>();
            services.AddTransient<MembershipFeatureAddViewModel>();
            services.AddTransient<FeatureViewModel>();
            services.AddTransient<FeatureAddViewModel>();
            services.AddTransient<MembershipFeatureViewModel>();
            services.AddTransient<AddClassBookingViewModel>();
            services.AddTransient<StaffViewModel>();
            services.AddTransient<StaffAddViewModel>();
            services.AddTransient<StaffDetailsViewModel>();
            
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type,ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            services.AddHttpClient<AuthHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/auth/");
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient<GymClassHtppClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/gymClass/");
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient<GeneralGymDetailsHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/generalGymDetail/");
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient<ContractHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/contract/");
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient<MembershipHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/membership/");
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient<ClientHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/client/");
            });
            services.AddHttpClient<TerminationHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/termination/");
            });
            services.AddHttpClient<ClientMembershipHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/clientMemberships/");
            });
            services.AddHttpClient<TrainerHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/trainer/");
            });
            services.AddHttpClient<ScheduledClassHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/scheduledClass/");
            });
            services.AddHttpClient<ClassBookingHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/classBooking/");
            });
            services.AddHttpClient<PersonalBookingHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/personalBooking/");
            });
            services.AddHttpClient<EmployeeHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/employee/");
            });
            services.AddHttpClient<EmploymentTerminationHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/employmentTermination/");
            });
           
            services.AddHttpClient<VisitHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/visit/");
            });
            services.AddHttpClient<MembershipPriceHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/membershipPrice/");
            });
            services.AddHttpClient<FeatureHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/feature/");
            });
            services.AddHttpClient<StaffHttpClient>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5105/api/person/");
            });
           
            _serviceProvider = services.BuildServiceProvider();
        }
        protected async override void OnStartup(StartupEventArgs e)
        {
            GeneralGymDetailsHttpClient gymDetailsHttpClient = _serviceProvider.GetRequiredService<GeneralGymDetailsHttpClient>();
            GeneralGymUpdateRequest gymDetails =  await gymDetailsHttpClient.GetGeneralGymSettingsAsync();
            Application.Current.Resources["GymName"] = gymDetails.GymName;
            Application.Current.Resources["Address"] = gymDetails.Address;
            Application.Current.Resources["ContactNumber"] = gymDetails.ContactNumber;
            Application.Current.Resources["PrimaryColor"] = (SolidColorBrush)(new BrushConverter()).ConvertFromString(gymDetails.PrimaryColor)!;
            Application.Current.Resources["SecondColor"] = (SolidColorBrush)(new BrushConverter()).ConvertFromString(gymDetails.SecondColor)!;
            Application.Current.Resources["BackgoundColor"] = (SolidColorBrush)(new BrushConverter()).ConvertFromString(gymDetails.BackgroundColor)!;
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
