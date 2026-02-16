using Microsoft.AspNetCore.Identity;

namespace GymManagementSystem.Core.Domain.Identity;

public class User : IdentityUser<Guid>
{
    public Guid? ClientId { get; set; }
    public Guid? PersonId { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpirationDateTime { get; set; }
    public bool MustChangePassword { get; set; }
}
