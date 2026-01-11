namespace GymManagementSystem.Core.DTO.ClassBooking;
public class ClassBookingAddRequest
{
    public Guid ScheduledClassId { get; set; }
    public Guid ClientId { get; set; }
    public bool IsRequestFromWeb { get; set; } = false;
}
