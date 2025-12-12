using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Client;

public class ClientDetailsResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ClientMembershipShortResponse? ClientMembership { get; set; }
}
