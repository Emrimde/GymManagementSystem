using FluentValidation;
using GymManagementSystem.API.Controllers;
using GymManagementSystem.Core.DTO.Client.QueryDto;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.Services;
using GymManagementSystem.Core.Validators.Client.QueryValidators;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagementSystem.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IMembershipPriceService, MembershipPriceService>();
        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IVisitService, VisitService>();
        services.AddTransient<IJwtService, JwtService>();
        services.AddScoped<IClientMembershipService, ClientMembershipService>();
        services.AddScoped<ITerminationService, TerminationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGeneralGymDetailsService, GeneralGymDetailService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ITrainerScheduleService, TrainerScheduleService>();
        services.AddScoped<IGymClassService, GymClassService>();
        services.AddScoped<IScheduledClassService, ScheduledClassService>();
        services.AddScoped<IClassBookingService, ClassBookingService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IPersonalBookingService, PersonalBookingService>();
        services.AddScoped<IEmploymentTerminationService, EmploymentTerminationService>();
        services.AddScoped<IMembershipFeatureService, MembershipFeatureService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IPersonStatusService, PersonStatusService>();
        services.AddScoped<IScheduleGeneratorService, ScheduleGeneratorService>();
        services.AddValidatorsFromAssemblyContaining<GetClientQueryDtoValidator>();

        return services;
    }
}
