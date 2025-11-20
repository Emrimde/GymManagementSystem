namespace GymManagementSystem.Core.Domain.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    //public bool IsStudent { get; set; }
    //public bool IsSenior { get; set; }
    public string StreetAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    //public Membership? Membership { get; set; }
    //public Guid? MembershipId { get; set; }
    public ICollection<Termination> Terminations { get; set; } = new List<Termination>();
    public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
}
