using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Core.DTO.Auth;
public class SignInDto
{
    public string? Username { get; set; }

    public string? Email { get; set; } 

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = default!;
}