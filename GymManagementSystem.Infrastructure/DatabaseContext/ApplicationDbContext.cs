using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.Enum;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.DatabaseContext;

public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public DbSet<Contract> Contracts { get; set; }
    public DbSet<GeneralGymDetail> GeneralGymDetails { get; set; }
    public DbSet<Termination> Terminations { get; set; }
    public DbSet<ClientMembership> ClientMemberships { get; set; }
    public DbSet<GymClass> GymClasses { get; set; }
    public DbSet<ScheduledClass> ScheduledClasses { get; set; }
    public DbSet<ClassBooking> ClassBookings { get; set; }
    public DbSet<PersonalBooking> PersonalBookings { get; set; }
    public DbSet<TrainerTimeOff> TrainerTimeOff { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<TrainerProfile> TrainerProfiles { get; set; }
    public DbSet<TrainerRate> TrainerRates { get; set; }
    public DbSet<TrainerContract> TrainerContracts { get; set; }
    public DbSet<EmploymentTermination> EmploymentTerminations { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<MembershipPrice> MembershipPrices { get; set; }
    public DbSet<MembershipFeature> MembershipFeatures { get; set; }
    public DbSet<Feature> Features { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<GeneralGymDetail>()
            .HasData(new GeneralGymDetail
            {
                Id = Guid.NewGuid(),
                GymName = "NextLevelGym",
                Address = "123 Fitness St, Muscle City",
                ContactNumber = "123456789",
                BackgroundColor = "#363740",
                PrimaryColor = "#EEEEEE",
                SecondColor = "#9AAD00",
                OpenTime = new TimeSpan(7, 0, 0),
                CloseTime = new TimeSpan(22, 0, 0),
                DefaultRate60 = 100m,
                DefaultRate120 = 150m,
                DefaultRate90 = 120m,
                DefaultGroupClassRate = 60m
            });

        builder.Entity<Membership>().HasData(
            new Membership
            {
                Id = new Guid("18EC8725-C23B-4EA4-90D4-2952E3B110A0"),
                Name = "Silver Membership",
                IsVisibleOffer = true,
                MembershipType = MembershipTypeEnum.Monthly,

            },
            new Membership
            {
                Id = new Guid("BEDD6962-6FA4-435D-8505-B7C6092B9875"),
                Name = "Gold Membership",
                IsVisibleOffer = true,
                MembershipType = MembershipTypeEnum.Monthly,
            },
              new Membership
              {
                  Id = new Guid("62DD1607-FD54-4186-B282-8EF9D82CDDCF"),
                  Name = "Silver Membership",
                  IsVisibleOffer = true,
                  MembershipType = MembershipTypeEnum.Annual,

              },
            new Membership
            {
                Id = new Guid("DB4A0DC9-6D66-445F-8AE1-E5B941E873CF"),
                Name = "Gold Membership",
                IsVisibleOffer = true,
                MembershipType = MembershipTypeEnum.Annual,
            }
        );
        builder.Entity<MembershipPrice>().HasData(
            new MembershipPrice
            {
                Id = Guid.NewGuid(),
                MembershipId = new Guid("18EC8725-C23B-4EA4-90D4-2952E3B110A0"),
                ValidFrom = DateTime.UtcNow,
                ValidTo = null,
                Price = 100m
            },
            new MembershipPrice
            {
                Id = Guid.NewGuid(),
                MembershipId = new Guid("BEDD6962-6FA4-435D-8505-B7C6092B9875"),
                ValidFrom = DateTime.UtcNow,
                ValidTo = null,
                Price = 150m
            },
            new MembershipPrice
            {
                Id = Guid.NewGuid(),
                MembershipId = new Guid("62DD1607-FD54-4186-B282-8EF9D82CDDCF"),
                ValidFrom = DateTime.UtcNow,
                ValidTo = null,
                Price = 1000m
            },
            new MembershipPrice
            {
                Id = Guid.NewGuid(),
                MembershipId = new Guid("DB4A0DC9-6D66-445F-8AE1-E5B941E873CF"),
                ValidFrom = DateTime.UtcNow,
                ValidTo = null,
                Price = 1500m
            }
            );

        builder.Entity<Feature>().HasData(
            new Feature()
            {
                Id = new Guid("E00CBD24-7E96-4074-85C9-10438A662C89"),
                BenefitDescription = "access to all training areas",
               
            },
            new Feature()
            {
                Id = new Guid("3DAF4CDB-EAC4-4B11-B63A-F08A2B2FB5F9"),
                BenefitDescription = "one-time one-hour coaching consultation",
                
            },
            new Feature()
            {
                Id = new Guid("265C36B0-C3B6-4A1D-A374-9769E7C0E2B7"),
                BenefitDescription = "fitness classes included in the price of the pass"
            },
            new Feature()
            {
                Id = new Guid("4ADCFE6C-8B0A-440E-9EB3-9FD10A78D82F"),
                BenefitDescription = "1 hour of personal training every 6 months"
            },
            new Feature()
            {
                Id = new Guid("724EC9BC-C40C-43CC-A5D8-6704EB2626D8"),
                BenefitDescription = "going to training with a friend 3 times a month"
            },
            new Feature()
            {
                Id = new Guid("4647B50E-5D18-4920-BD4F-953BA60E033D"),
                BenefitDescription = "the possibility to book group classes 7 days in advance"
            }

            );
    }
}
