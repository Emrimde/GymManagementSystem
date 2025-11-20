using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.Core.DTO.Auth;
public class SignInDto
{
    [Required(ErrorMessage = "Username is required.")]
    public string Username { get; set; } = default!;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = default!;
}