namespace GymManagementSystem.Core.WebDTO.Client;
public class ClientWebAddRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
}
