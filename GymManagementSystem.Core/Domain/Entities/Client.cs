using GymManagementSystem.Core.Domain.Identity;

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
    public User? User { get; set; }
    public bool IsActive { get; set; } = false;
    public bool? HasParentalConsent { get; set; }
    //public ICollection<Termination> Terminations { get; set; } = new List<Termination>();
    public ICollection<Visit> Visits { get; set; } = new List<Visit>();
    public ICollection<ClientMembership> ClientMemberships { get; set; } = new List<ClientMembership>();
    public ICollection<ClassBooking> ClassBookings { get; set; } = new List<ClassBooking>();
    public ICollection<PersonalBooking> PersonalBookings { get; set; } = new List<PersonalBooking>();
}
