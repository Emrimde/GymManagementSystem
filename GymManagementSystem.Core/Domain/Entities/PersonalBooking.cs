namespace GymManagementSystem.Core.Domain.Entities;
public enum BookingStatus
{
    Booked,
    Cancelled,
    PaidByClient
}
public class PersonalBooking
{
    public Guid Id { get; set; }
    public Guid TrainerContractId { get; set; }
    public Guid ClientId { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Booked;
    public decimal Price { get; set; }
    public TrainerContract? TrainerContract { get; set; }
    public Client? Client { get; set; }
}
