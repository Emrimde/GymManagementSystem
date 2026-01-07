namespace GymManagementSystem.Core.WebDTO.Client;
public class ClientWebUpdateRequest
{
    public string PhoneNumber { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
}
