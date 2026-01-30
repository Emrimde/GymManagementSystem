using GymManagementSystem.Core.DTO.ClientMembership;

namespace GymManagementSystem.Core.DTO.Client;

public class ClientDetailsResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string DateOfBirth { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string? Valid { get; set; } = default!;
    public string? ClientMembershipName { get; set; } = default!;
    public bool IsActive { get; set; }
    public bool CanTerminate { get; set; }
    public int TotalVisits { get; set; }
    public string? LastVisitDate { get; set; }
}

    //public ClientMembershipShortResponse? ClientMembership { get; set; }