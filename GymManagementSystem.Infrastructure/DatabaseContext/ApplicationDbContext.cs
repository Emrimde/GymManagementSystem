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
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<GeneralGymDetail>()
            .HasData(new GeneralGymDetail
            {
                Id = new Guid("65893784-DA50-4C63-AE01-9704C5D5EDA3"),
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
                DefaultGroupClassRate = 60m,
                AboutUs = "We are a place created for people who want to truly improve their health, physique, and well-being — not just “tick off” a workout. Our goal is to build a strong, capable, and mindful community where everyone, regardless of their level, feels welcome. We combine modern equipment with expert coaching to make training not only hard, but smart. We focus on quality of movement, steady progress, and safety, because long-term results matter more than quick fixes. We help our members set clear goals and achieve them step by step.\r\n\r\nWe don’t believe in shortcuts — we believe in building lasting habits and real lifestyle change. We create an environment where training becomes part of everyday life, not a burden. We believe that a strong body builds a strong mind. That’s why we support, motivate, and educate — not just count reps. Our gym is more than equipment; it’s people, atmosphere, and a shared drive to be better than yesterday.",
                LogoUrl = "http://localhost:5105/uploads/logos/logo_d8faf809-8917-4ddd-b78e-618df23cf5c8.png",
                Nip = "123456789"
            });

        builder.Entity<Membership>().HasData(
            new Membership
            {
                Id = new Guid("18EC8725-C23B-4EA4-90D4-2952E3B110A0"),
                Name = "Silver Membership",
                MembershipType = MembershipTypeEnum.Monthly,
                ClassBookingDaysInAdvanceCount = 7,
                FreeFriendEntryCountPerMonth = 3,
            },
            new Membership
            {
                Id = new Guid("BEDD6962-6FA4-435D-8505-B7C6092B9875"),
                Name = "Gold Membership",
                MembershipType = MembershipTypeEnum.Monthly,
                ClassBookingDaysInAdvanceCount = 14,
                FreeFriendEntryCountPerMonth = 6,

            },
              new Membership
              {
                  Id = new Guid("62DD1607-FD54-4186-B282-8EF9D82CDDCF"),
                  Name = "Silver Membership",
                  MembershipType = MembershipTypeEnum.Annual,
                  ClassBookingDaysInAdvanceCount = 7,
                  FreeFriendEntryCountPerMonth = 3,
              },
            new Membership
            {
                Id = new Guid("DB4A0DC9-6D66-445F-8AE1-E5B941E873CF"),
                Name = "Gold Membership",
                MembershipType = MembershipTypeEnum.Annual,
                ClassBookingDaysInAdvanceCount = 14,
                FreeFriendEntryCountPerMonth = 6,
            }
        );

        builder.Entity<MembershipPrice>().HasData(
            new MembershipPrice
            {
                Id = new Guid("0D29E7B7-A940-4187-8A74-76BA99AF8B1F"),
                MembershipId = new Guid("18EC8725-C23B-4EA4-90D4-2952E3B110A0"),
                ValidFrom = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ValidTo = null,
                Price = 100m
            },
            new MembershipPrice
            {
                Id = new Guid("1260207E-C0E3-4503-BB31-A5EF13E5CEE8"),
                MembershipId = new Guid("BEDD6962-6FA4-435D-8505-B7C6092B9875"),
                ValidFrom = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ValidTo = null,
                Price = 150m
            },
            new MembershipPrice
            {
                Id = new Guid("F23A5317-8F53-4FC2-BFBA-661728098038"),
                MembershipId = new Guid("62DD1607-FD54-4186-B282-8EF9D82CDDCF"),
                ValidFrom = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ValidTo = null,
                Price = 1000m
            },
            new MembershipPrice
            {
                Id = new Guid("6707ECBD-F2C1-49EE-AE24-8CE497A4F501"),
                MembershipId = new Guid("DB4A0DC9-6D66-445F-8AE1-E5B941E873CF"),
                ValidFrom = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                ValidTo = null,
                Price = 1500m
            }
            );

        builder.Entity<MembershipFeature>().HasData(
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("18EC8725-C23B-4EA4-90D4-2952E3B110A0"),
        FeatureDescription = "Full access to all training zones"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("18EC8725-C23B-4EA4-90D4-2952E3B110A0"),
        FeatureDescription = "Group classes included in the membership price"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("BEDD6962-6FA4-435D-8505-B7C6092B9875"),
        FeatureDescription = "Full access to all training zones"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("BEDD6962-6FA4-435D-8505-B7C6092B9875"),
        FeatureDescription = "Group classes included in the membership price"
    },

    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("62DD1607-FD54-4186-B282-8EF9D82CDDCF"),
        FeatureDescription = "Full access to all training zones"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("62DD1607-FD54-4186-B282-8EF9D82CDDCF"),
        FeatureDescription = "Group classes included in the membership price"
    },

    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("DB4A0DC9-6D66-445F-8AE1-E5B941E873CF"),
        FeatureDescription = "Full access to all training zones"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("DB4A0DC9-6D66-445F-8AE1-E5B941E873CF"),
        FeatureDescription = "Group classes included in the membership price"
    }, new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("18EC8725-C23B-4EA4-90D4-2952E3B110A0"),
        FeatureDescription = "Can book group classes up to 7 days in advance"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("18EC8725-C23B-4EA4-90D4-2952E3B110A0"),
        FeatureDescription = "Can invite a friend 3 times per month"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("18EC8725-C23B-4EA4-90D4-2952E3B110A0"),
        FeatureDescription = "Includes 1 free personal training session every 6 months"
    },

    // Gold Monthly
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("BEDD6962-6FA4-435D-8505-B7C6092B9875"),
        FeatureDescription = "Can book group classes up to 14 days in advance"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("BEDD6962-6FA4-435D-8505-B7C6092B9875"),
        FeatureDescription = "Can invite a friend 6 times per month"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("BEDD6962-6FA4-435D-8505-B7C6092B9875"),
        FeatureDescription = "Includes 2 free personal training sessions every 6 months"
    },

    // Silver Annual
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("62DD1607-FD54-4186-B282-8EF9D82CDDCF"),
        FeatureDescription = "Can book group classes up to 7 days in advance"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("62DD1607-FD54-4186-B282-8EF9D82CDDCF"),
        FeatureDescription = "Can invite a friend 3 times per month"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("62DD1607-FD54-4186-B282-8EF9D82CDDCF"),
        FeatureDescription = "Includes 1 free personal training session every 6 months"
    },

    // Gold Annual
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("DB4A0DC9-6D66-445F-8AE1-E5B941E873CF"),
        FeatureDescription = "Can book group classes up to 14 days in advance"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("DB4A0DC9-6D66-445F-8AE1-E5B941E873CF"),
        FeatureDescription = "Can invite a friend 6 times per month"
    },
    new MembershipFeature
    {
        Id = Guid.NewGuid(),
        MembershipId = new Guid("DB4A0DC9-6D66-445F-8AE1-E5B941E873CF"),
        FeatureDescription = "Includes 2 free personal training sessions every 6 months"
    }

);

    }
}
