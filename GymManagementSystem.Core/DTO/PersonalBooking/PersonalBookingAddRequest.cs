namespace GymManagementSystem.Core.DTO.PersonalBooking;

public class PersonalBookingAddRequest
{
    public Guid TrainerId { get;  set; }
    public Guid ClientId { get;  set; }
    public DateTime Start { get;  set; }
    public DateTime End { get;  set; }
}