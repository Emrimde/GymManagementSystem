namespace GymManagementSystem.Core.DTO.PersonalBooking;
public class PersonalBookingForEditResponse
{
    public Guid ClientId { get; set; }
    public Guid TrainerId { get; set; }
    public Guid TrainerRateId { get; set; }

    public DateTime StartDate { get; set; }
    
}
