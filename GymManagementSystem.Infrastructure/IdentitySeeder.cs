using GymManagementSystem.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<Role>>();
        var userManager = services.GetRequiredService<UserManager<User>>();

        string[] roles = { "Receptionist", "Trainer", "Manager", "Client", "Owner" };

        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new Role { Name = role });

        var ownerEmail = "owner@gym.local";
        var owner = await userManager.FindByEmailAsync(ownerEmail);

        if (owner == null)
        {
            owner = new User
            {
                UserName = ownerEmail,
                Email = ownerEmail,
                EmailConfirmed = true
            };

            await userManager.CreateAsync(owner, "Owner123!");
            await userManager.AddToRoleAsync(owner, "Owner");
        }
    }
}

