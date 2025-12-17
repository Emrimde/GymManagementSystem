using GymManagementSystem.API.Controllers;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagementSystem.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IMembershipService, MembershipService>();
        services.AddScoped<IVisitService, VisitService>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IClientMembershipService, ClientMembershipService>();
        services.AddScoped<ITerminationService, TerminationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGeneralGymDetailsService, GeneralGymDetailService>();
        services.AddScoped<ITerminationValidator, TerminationValidatorService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ITrainerScheduleService, TrainerScheduleService>();
        services.AddScoped<IGymClassService, GymClassService>();
        services.AddScoped<IScheduledClassService, ScheduledClassService>();
        services.AddScoped<IClassBookingService, ClassBookingService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IPersonalBookingService, PersonalBookingService>();
        services.AddScoped<IEmploymentTerminationService, EmploymentTerminationService>();
       
        return services;
    }
}
