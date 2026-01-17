namespace GymManagementSystem.Core.DTO.Client;

public class ClientResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
}
