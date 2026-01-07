namespace GymManagementSystem.Core.WebDTO.ClientMembership;
public class ClientMembershipWebPreviewResponse
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string MembershipName { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string? Street { get; set; }
    public string? City { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
