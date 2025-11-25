using GymManagementSystem.API.Controllers;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.ServiceContracts;
using GymManagementSystem.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagementSystem.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService<Client>>();
        services.AddScoped<IService<MembershipResponse, MembershipAddRequest, MembershipUpdateRequest, Membership>, MembershipService<Membership>>();
        services.AddScoped<IContractService, ContractService<Contract>>();
        services.AddScoped<IClientMembershipService, ClientMembershipService<ClientMembership>>();
        services.AddScoped<IServiceAdder<TerminationResponse, TerminationAddRequest>, TerminationService>();
        services.AddScoped<IServiceReader<TerminationResponse>, TerminationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IGeneralGymDetailsService, GeneralGymDetailService>();
        services.AddScoped<ITerminationValidator, TerminationValidatorService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<IGymClassService, GymClassService>();
        services.AddScoped<IScheduledClassService, ScheduledClassService>();
        services.AddScoped<IClassBookingService, ClassBookingService>();
        return services;
    }
}
