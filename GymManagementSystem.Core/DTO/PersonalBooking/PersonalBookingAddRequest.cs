namespace GymManagementSystem.Core.DTO.PersonalBooking;

public class PersonalBookingAddRequest
{
    public Guid TrainerId { get;  set; }
    public Guid TrainerRateId { get; set; }
    public Guid ClientId { get;  set; }
    public DateTime StartDay { get;  set; }
    public TimeSpan StartHour { get; set; }
}