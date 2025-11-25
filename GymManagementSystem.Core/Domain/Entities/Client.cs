namespace GymManagementSystem.Core.Domain.Entities;

public class Client
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string StreetAddress { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public ICollection<Termination> Terminations { get; set; } = new List<Termination>();
    public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
    public ICollection<ClassBooking> ClassBookings { get; set; } = new List<ClassBooking>();
}
