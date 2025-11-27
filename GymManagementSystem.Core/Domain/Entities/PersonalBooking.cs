namespace GymManagementSystem.Core.Domain.Entities;
public enum BookingStatus
{
    Booked,
    Cancelled
}
public class PersonalBooking
{
    public Guid Id { get; set; }
    public Guid TrainerId { get; set; }
    public Guid ClientId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Booked;
    public Trainer? Trainer { get; set; }
    public Client? Client { get; set; }
}
