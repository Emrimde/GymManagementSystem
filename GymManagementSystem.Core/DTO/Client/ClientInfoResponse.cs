namespace GymManagementSystem.Core.DTO.Client;
public class ClientInfoResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}
