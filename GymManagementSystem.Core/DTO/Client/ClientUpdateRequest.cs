namespace GymManagementSystem.Core.DTO.Client;
public class ClientUpdateRequest
{
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}
