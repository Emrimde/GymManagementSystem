using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Core.Domain.Identity;

public class User : IdentityUser<Guid>
{
    public Guid? ClientId { get; set; }
    public bool MustChangePassword { get; set; }
}
