using GymManagementSystem.Core.Domain;
using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GymManagementSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        //services.AddScoped<IRepository<Client>, ClientRepository>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IMembershipFeatureRepository, MembershipFeatureRepository>();

        services.AddScoped<IMembershipPriceRepository, MembershipPriceRepository>();
        services.AddScoped<IVisitRepository, VisitRepository>();
        services.AddScoped<IClientMembershipRepository, ClientMembershipRepository>();
        services.AddScoped<IRepository<MembershipResponse,Membership>, MembershipRepository>();
        services.AddScoped<IRepository<ContractResponse,Contract>, ContractRepository>();
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<IRepository<TerminationResponse,Termination>, TerminationRepository>();
        services.AddScoped<IGeneralGymRepository, GeneralGymDetailsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<IRepository<ClientMembershipResponse,ClientMembership>, ClientMembershipRepository>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        //services.AddScoped<IRepository<GymClassResponse,GymClass>, GymClassRepository>();
        services.AddScoped<IScheduledClassRepository, ScheduledClassRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IRepository<ClassBookingResponse,ClassBooking>, ClassBookingRepository>();
        services.AddScoped<ITrainerTimeOffRepository, TrainerTimeOffRepository>();
        services.AddScoped<IPersonalBookingRepository, PersonalBookingRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<ITrainerRateRepository, TrainerRateRepository>();
        services.AddScoped<IEmploymentTerminationRepository, EmploymentTerminationRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IGymClassRepository, GymClassRepository>();
        services.AddScoped<IFeatureRepository, FeatureRepository>();
        
        return services;
    }
}
