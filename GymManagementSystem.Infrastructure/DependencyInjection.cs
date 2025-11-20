using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagementSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Client>, ClientRepository>();
        services.AddScoped<IRepository<Membership>, MembershipRepository>();
        services.AddScoped<IRepository<Contract>, ContractRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IRepository<Termination>, TerminationRepository>();
        services.AddScoped<IGeneralGymRepository, GeneralGymDetailsRepository>();
        services.AddScoped<IRepository<ClientMembership>, ClientMembershipRepository>();
        services.AddScoped<IRepository<Trainer>, TrainerRepository>();
        services.AddScoped<IRepository<GymClass>, GymClassRepository>();
        return services;
    }
}
