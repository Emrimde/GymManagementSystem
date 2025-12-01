namespace GymManagementSystem.Core.Domain.Entities;
public class TrainerProfile
{
    public Guid Id { get; set; }    
    public Guid EmployeeId { get; set; }
    public ICollection<PersonalBooking> PersonalBookings { get; set; } = new List<PersonalBooking>();
}
