namespace GymManagementSystem.Core.DTO.Client;
public class ClientAddRequest
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
}
