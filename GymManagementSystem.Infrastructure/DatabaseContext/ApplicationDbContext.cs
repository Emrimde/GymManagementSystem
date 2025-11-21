using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.Identity;
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
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<GymClass> GymClasses { get; set; }
    public DbSet<ScheduledClass> ScheduledClasses { get; set; }
    public DbSet<ClassBooking> ClassBookings { get; set; }

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
                SecondColor = "#9AAD00"
            });
    }
}
